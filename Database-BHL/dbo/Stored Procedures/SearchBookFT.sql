
CREATE PROCEDURE [dbo].[SearchBookFT]

@Title			nvarchar(2000) = '',
@AuthorLastName	nvarchar(255) = '',
@Volume			nvarchar(100) = '',
@Edition		nvarchar(450) = '',
@Year			smallint = NULL,
@Subject		nvarchar(50) = '',
@LanguageCode	nvarchar(10) = '',
@CollectionID	int = null,
@ReturnCount	int = 100,
@SortBy			nvarchar(50) = 'Rank'

AS 

BEGIN

SET NOCOUNT ON

-- Revert to 'normal' SQL search of the search catalog is offline
DECLARE @CatalogStatus int
exec @CatalogStatus = dbo.SearchCatalogCheckStatus
IF (@CatalogStatus = 0)
BEGIN
	exec dbo.SearchBook @Title, @AuthorLastName, @Volume, @Edition, @Year, @Subject,
						@LanguageCode, @CollectionID, @ReturnCount, @SortBy
	RETURN
END

DECLARE @SearchTitle nvarchar(4000)
DECLARE @SearchAuthorLastName nvarchar(4000)
DECLARE @SearchVolume nvarchar(4000)
DECLARE @SearchEdition nvarchar(4000)
DECLARE @SearchSubject nvarchar(4000)

-- Transform the search terms into full-text search phrases
SELECT @SearchTitle = dbo.fnGetFullTextSearchString(@Title)
SELECT @SearchAuthorLastName = dbo.fnGetFullTextSearchString(@AuthorLastName)
SELECT @SearchVolume = dbo.fnGetFullTextSearchString(@Volume)
SELECT @SearchEdition = dbo.fnGetFullTextSearchString(@Edition)
SELECT @SearchSubject = dbo.fnGetFullTextSearchString(@Subject)

-- Get initial list of items, filtered by Language
SELECT	i.ItemID
INTO	#tmpActiveItem
FROM	dbo.Item i WITH (NOLOCK)
		LEFT JOIN dbo.ItemLanguage il WITH (NOLOCK) ON i.ItemID = il.ItemID AND il.LanguageCode = @LanguageCode
WHERE	ItemStatusID = 40
AND		(i.LanguageCode = @LanguageCode OR
		 ISNULL(il.LanguageCode, '') = @LanguageCode OR
		@LanguageCode = '')

-- Filter items by volume
SELECT	i.ItemID, x.[RANK] AS ItemRank
INTO	#tmpItem
FROM	CONTAINSTABLE(SearchCatalog, Volume, @SearchVolume) x
		INNER JOIN SearchCatalog c WITH (NOLOCK) ON c.SearchCatalogID = x.[KEY]
		INNER JOIN #tmpActiveItem i ON c.ItemID = i.ItemID
UNION
SELECT ItemID, 0 FROM #tmpActiveItem WHERE @SearchVolume = '"**"'

-- Get initial list of Titles, filtered by Language
SELECT	t.TitleID, 
		ti.ItemSequence,
		i.ItemID,
		i.ItemRank
INTO	#tmpActiveTitle
FROM	dbo.Title t WITH (NOLOCK)
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON t.TitleID = ti.TitleID
		INNER JOIN #tmpItem i ON ti.ItemID = i.ItemID
		LEFT JOIN dbo.TitleLanguage tl WITH (NOLOCK) ON t.TitleID = tl.TitleID AND tl.LanguageCode = @LanguageCode
WHERE	t.PublishReady = 1
AND		(t.LanguageCode = @LanguageCode OR
			ISNULL(tl.LanguageCode, '') = @LanguageCode OR
			@LanguageCode = '')

-- Add titles by title string
SELECT	tmp.TitleID, 
		tmp.ItemSequence,
		tmp.ItemID,
		tmp.ItemRank,
		x.[RANK] AS TitleRank
INTO	#tmpTitle
FROM	CONTAINSTABLE(SearchCatalog, (FullTitle, Associations, Variants, UniformTitle), @SearchTitle) x
		INNER JOIN SearchCatalog c WITH (NOLOCK) ON c.SearchCatalogID = x.[KEY]
		INNER JOIN #tmpActiveTitle tmp ON c.TitleID = tmp.TitleID AND c.ItemID = tmp.ItemID
UNION
SELECT TitleID, ItemSequence, ItemID, ItemRank, 0 FROM #tmpActiveTitle WHERE @SearchTitle = '"**"'


-- If a volume was specified, then add any items that match everything *except*
-- volume to our initial list.  We want users to know that we have a title, even if
-- we don't have the exact volume that they asked for.  This also helps if the volume
-- data is messy (maybe we DO have the volume, but the initial search didn't find it).
IF (@SearchVolume <> '"**"' AND @SearchTitle <> '"**"')
BEGIN
	INSERT INTO #tmpTitle
	SELECT	t.TitleID, 
			ti.ItemSequence,
			i.ItemID,
			0 AS ItemRank,
			x.[RANK] AS TitleRank
	FROM	CONTAINSTABLE(SearchCatalog, (FullTitle, Associations, Variants, UniformTitle), @SearchTitle) x
			INNER JOIN SearchCatalog c WITH (NOLOCK) ON c.SearchCatalogID = x.[KEY]
			INNER JOIN dbo.Title t WITH (NOLOCK) ON c.TitleID = t.TitleID
			INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON t.TitleID = ti.TitleID
			INNER JOIN dbo.Item i WITH (NOLOCK) ON ti.ItemID = i.ItemID
			LEFT JOIN dbo.TitleLanguage tl WITH (NOLOCK) ON t.TitleID = tl.TitleID AND tl.LanguageCode = @LanguageCode
			LEFT JOIN dbo.ItemLanguage il WITH (NOLOCK) ON i.ItemID = il.ItemID AND il.LanguageCode = @LanguageCode
	WHERE	t.PublishReady = 1
	AND		(t.LanguageCode = @LanguageCode OR
				i.LanguageCode = @LanguageCode OR
				ISNULL(tl.LanguageCode, '') = @LanguageCode OR
				ISNULL(il.LanguageCode, '') = @LanguageCode OR
				@LanguageCode = '')
	AND		i.ItemStatusID = 40
	AND		t.TitleID NOT IN (SELECT TitleID FROM #tmpTitle)
END

-- Filter by Edition
SELECT DISTINCT
		tmp.TitleID,
		tmp.ItemSequence,
		tmp.ItemID,
		tmp.ItemRank,
		tmp.TitleRank,
		x.[RANK] as EditionRank
INTO	#tmpTitleFilter0
FROM	CONTAINSTABLE(SearchCatalog, EditionStatement, @SearchEdition) x
		INNER JOIN SearchCatalog c WITH (NOLOCK) ON c.SearchCatalogID = x.[KEY]
		INNER JOIN #tmpTitle tmp ON c.TitleID = tmp.TitleID AND c.ItemID = tmp.ItemID
UNION
SELECT DISTINCT TitleID, ItemSequence, ItemID, ItemRank, TitleRank, 0 From #tmpTitle WHERE @SearchEdition = '"**"'

-- Filter by Year
SELECT DISTINCT
		tmp.TitleID,
		tmp.ItemSequence,
		tmp.ItemID,
		tmp.ItemRank,
		tmp.TitleRank,
		tmp.EditionRank
INTO	#tmpTitleFilter1
FROM	#tmpTitleFilter0 tmp INNER JOIN dbo.Title t WITH (NOLOCK) ON tmp.TitleID = t.TitleID
WHERE	(@Year IS NULL OR 
			ISNULL(t.StartYear, 0) = @Year OR 
			ISNULL(t.EndYear, 0) = @Year OR 
			@Year BETWEEN ISNULL(t.StartYear, 0) and ISNULL(t.EndYear, 0))

-- Filter by author
SELECT DISTINCT
		tmp.TitleID,
		tmp.ItemSequence,
		tmp.ItemID,
		tmp.ItemRank,
		tmp.TitleRank,
		tmp.EditionRank,
		x.[RANK] AS AuthorRank
INTO	#tmpTitleFilter2
FROM	CONTAINSTABLE(SearchCatalog, Authors, @SearchAuthorLastName) x
		INNER JOIN SearchCatalog c WITH (NOLOCK) ON c.SearchCatalogID = x.[KEY]
		INNER JOIN #tmpTitleFilter1 tmp ON c.TitleID = tmp.TitleID AND c.ItemID = tmp.ItemID
UNION
SELECT DISTINCT TitleID, ItemSequence, ItemID, ItemRank, TitleRank, EditionRank, 0 FROM #tmpTitleFilter1 WHERE @SearchAuthorLastName = '"**"'

-- Filter by subject
SELECT DISTINCT
		tmp.TitleID,
		tmp.ItemSequence,
		tmp.ItemID,
		tmp.ItemRank,
		tmp.TitleRank,
		tmp.EditionRank,
		tmp.AuthorRank,
		x.[RANK] AS SubjectRank
INTO	#tmpTitleFilter3
FROM	CONTAINSTABLE(SearchCatalog, Subjects, @SearchSubject) x
		INNER JOIN SearchCatalog c WITH (NOLOCK) ON c.SearchCatalogID = x.[KEY]
		INNER JOIN #tmpTitleFilter2 tmp ON c.TitleID = tmp.TitleID AND c.ItemID = tmp.ItemID
UNION
SELECT DISTINCT TitleID, ItemSequence, ItemID, ItemRank, TitleRank, EditionRank, AuthorRank, 0 FROM #tmpTitleFilter2 WHERE @SearchSubject = '"**"'


-- Filter by collection
CREATE TABLE #tmpTitleFinal
	(
	TitleID int NOT NULL,
	ItemSequence smallint NOT NULL,
	ItemRank int NULL,
	TitleRank int NULL,
	EditionRank int NULL,
	AuthorRank int NULL,
	SubjectRank int NULL
	)

-- Get first item of each title associated with a collection
INSERT	#tmpTitleFinal
SELECT	tmp.TitleID,
		MIN(tmp.ItemSequence) AS ItemSequence,
		0,0,0,0,0
FROM	#tmpTitleFilter3 tmp INNER JOIN dbo.TitleCollection tc WITH (NOLOCK) ON tmp.TitleID = tc.TitleID
		INNER JOIN dbo.Collection c WITH (NOLOCK) ON tc.CollectionID = c.CollectionID
WHERE	tc.CollectionID = @CollectionID
AND		c.Active = 1	-- collection must be active
AND		c.CollectionTarget IN ('All', 'BHL')  -- collection target must include BHL
GROUP BY
		tmp.TitleID

-- Get items directly associated with a collection
INSERT	#tmpTitleFinal
SELECT	tmp.TitleID,
		tmp.ItemSequence,
		0,0,0,0,0
FROM	#tmpTitleFilter3 tmp INNER JOIN dbo.ItemCollection ic WITH (NOLOCK) ON tmp.ItemID = ic.ItemID
		INNER JOIN dbo.Collection c WITH (NOLOCK) ON ic.CollectionID = c.CollectionID
WHERE	ic.CollectionID = @CollectionID
AND		c.Active = 1	-- collection must be active
AND		c.CollectionTarget IN ('All', 'BHL')  -- collection target must include BHL

-- Just get first items of titles when no collection specified
INSERT	#tmpTitleFinal
SELECT	tmp.TitleID,
		MIN(tmp.ItemSequence) AS ItemSequence,
		0,0,0,0,0
FROM	#tmpTitleFilter3 tmp 
WHERE	@CollectionID IS NULL
GROUP BY
		tmp.TitleID

-- Add the RANK values for each item
UPDATE	#tmpTitleFinal
SET		ItemRank = t.ItemRank,
		TitleRank = t.TitleRank,
		EditionRank = t.EditionRank,
		AuthorRank = t.AuthorRank,
		SubjectRank = t.SubjectRank
FROM	#tmpTitleFinal f INNER JOIN #tmpTitleFilter3 t
			ON f.TitleID = t.TitleID
			AND f.ItemSequence = t.ItemSequence

----------------------------------------------------------
-- Compile the final sortable result set
/*
-- NOTE: This was replaced by the following statements which remove duplicate items.
SELECT	tmp.TitleID,
		i.ItemID,
		ti.ItemSequence,
		t.FullTitle,
		t.SortTitle,
		t.PartNumber,
		t.PartName,
		t.EditionStatement,
		t.PublicationDetails,
		t.Datafield_260_a,
		t.Datafield_260_b,
		t.Datafield_260_c,
		i.Volume,
		dbo.fnCOinSAuthorStringForTitle(tmp.TitleID, 0) AS Authors,
		dbo.fnCollectionStringForTitleAndItem(tmp.TitleID, i.ItemID) AS Collections,
		dbo.fnSeriesStringForTitle (tmp.TitleID) AS Associations,
		inst.InstitutionName
INTO	#tmpSortable
FROM	#tmpTitleFinal tmp INNER JOIN dbo.Title t ON tmp.TitleID = t.TitleID
		INNER JOIN dbo.TitleItem ti ON t.TitleID = ti.TitleID AND ti.ItemSequence = tmp.ItemSequence
		INNER JOIN dbo.Item i ON ti.ItemID = i.ItemID AND i.ItemStatusID = 40
		LEFT JOIN dbo.Institution inst ON i.InstitutionCode = inst.InstitutionCode
*/
		
-- Compile the final sortable result set.  In doing so, see if any items appear more 
-- than once.  If any do, only show the primary title for those items.
SELECT	tmp.TitleID,
		i.PrimaryTitleID,
		i.ItemID,
		ti.ItemSequence,
		t.FullTitle,
		t.SortTitle,
		t.PartNumber,
		t.PartName,
		t.EditionStatement,
		t.PublicationDetails,
		t.Datafield_260_a,
		t.Datafield_260_b,
		t.Datafield_260_c,
		i.Volume,
		i.ExternalUrl,
		c.Authors,
		dbo.fnCollectionStringForTitleAndItem(tmp.TitleID, i.ItemID) AS Collections,
		dbo.fnSeriesStringForTitle (tmp.TitleID) AS Associations,
		inst.InstitutionName,
		TitleRank,
		ItemRank,
		AuthorRank,
		EditionRank,
		SubjectRank,
		(10 * TitleRank) + (20 * ItemRank) + (10 * AuthorRank) + (EditionRank) + (5 * SubjectRank) AS [Rank]
INTO	#tmpMayContainDups
FROM	#tmpTitleFinal tmp INNER JOIN dbo.Title t WITH (NOLOCK) ON tmp.TitleID = t.TitleID
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON t.TitleID = ti.TitleID AND ti.ItemSequence = tmp.ItemSequence
		INNER JOIN dbo.Item i WITH (NOLOCK) ON ti.ItemID = i.ItemID AND i.ItemStatusID = 40
		LEFT JOIN dbo.Institution inst WITH (NOLOCK) ON i.InstitutionCode = inst.InstitutionCode
		INNER JOIN dbo.SearchCatalog c ON t.TitleID = c.TitleID AND i.ItemID = c.ItemID

/*
-- Find any duplicated items
SELECT	ItemID
INTO	#tmpDups
FROM	#tmpMayContainDups
GROUP BY ItemID HAVING COUNT(*) > 1

-- Show all non-duplicate item information, plus the primary title information for duplicate items
SELECT	m.TitleID, m.PrimaryTitleID, m.ItemID, m.ItemSequence, m.FullTitle, m.SortTitle, m.PartNumber, m.PartName,
		m.EditionStatement, m.PublicationDetails, m.Datafield_260_a, m.Datafield_260_b, m.Datafield_260_c, m.Volume,
		m.ExternalUrl, m.Authors, m.Collections, m.Associations, m.InstitutionName, m.TitleRank, m.ItemRank, m.AuthorRank,
		m.EditionRank, m.SubjectRank, CONVERT(decimal(7,2), m.[Rank]) AS [Rank]
INTO	#tmpSortable
FROM	#tmpMayContainDups m LEFT JOIN #tmpDups d
			ON m.ItemID = d.ItemID
WHERE	d.ItemID IS NULL
UNION
SELECT	m.TitleID, m.PrimaryTitleID, m.ItemID, m.ItemSequence, m.FullTitle, m.SortTitle, m.PartNumber, m.PartName,
		m.EditionStatement, m.PublicationDetails, m.Datafield_260_a, m.Datafield_260_b, m.Datafield_260_c, m.Volume,
		m.ExternalUrl, m.Authors, m.Collections, m.Associations, m.InstitutionName, m.TitleRank, m.ItemRank, m.AuthorRank,
		m.EditionRank, m.SubjectRank, CONVERT(decimal(7,2), m.[Rank]) AS [Rank]
FROM	#tmpMayContainDups m INNER JOIN #tmpDups d
			ON m.ItemID = d.ItemID
WHERE	m.TitleID = PrimaryTitleID
*/

-- Find any duplicated titles
SELECT	TitleID, MIN(ItemID) AS ItemID
INTO	#tmpDups
FROM	#tmpMayContainDups
GROUP BY TitleID HAVING COUNT(*) > 1

-- Show all non-duplicate title information
SELECT	m.TitleID, m.PrimaryTitleID, m.ItemID, m.ItemSequence, m.FullTitle, m.SortTitle, m.PartNumber, m.PartName,
		m.EditionStatement, m.PublicationDetails, m.Datafield_260_a, m.Datafield_260_b, m.Datafield_260_c, m.Volume,
		m.ExternalUrl, m.Authors, m.Collections, m.Associations, m.InstitutionName, m.TitleRank, m.ItemRank, m.AuthorRank,
		m.EditionRank, m.SubjectRank, CONVERT(decimal(7,2), m.[Rank]) AS [Rank]
INTO	#tmpSortable
FROM	#tmpMayContainDups m LEFT JOIN #tmpDups d
			ON m.TitleID = d.TitleID AND m.ItemID <> d.ItemID
WHERE	d.ItemID IS NULL

-- De-emphasize ranking of any items contributed by Canadiana.org.
UPDATE	#tmpSortable
SET		[Rank] = [Rank] / 100.00
FROM	#tmpSortable s INNER JOIN dbo.Item i WITH (NOLOCK) ON s.ItemID = i.ItemID
WHERE	i.InstitutionCode = 'CANADIANA'

----------------------------------------------------------
-- Return the sorted result set
IF (@SortBy = 'Author')
BEGIN
	SELECT TOP (@ReturnCount)
			TitleID,
			ItemID,
			FullTitle,
			PartNumber,
			PartName,
			EditionStatement,
			PublicationDetails,
			Datafield_260_a,
			Datafield_260_b,
			Datafield_260_c,
			Volume,
			ExternalUrl,
			Authors,
			Collections,
			Associations,
			InstitutionName,
			TitleRank,
			ItemRank,
			AuthorRank,
			EditionRank,
			SubjectRank,
			[Rank]
	FROM	#tmpSortable
	ORDER BY
			Authors, 
			SortTitle, 
			ItemSequence
END
ELSE
BEGIN
	IF (@SortBy = 'Date')
	BEGIN
		SELECT TOP (@ReturnCount)
				s.TitleID,
				s.ItemID,
				s.FullTitle,
				s.PartNumber,
				s.PartName,
				s.EditionStatement,
				s.PublicationDetails,
				s.Datafield_260_a,
				s.Datafield_260_b,
				s.Datafield_260_c,
				s.Volume,
				s.ExternalUrl,
				s.Authors,
				s.Collections,
				s.Associations,
				s.InstitutionName,
				TitleRank,
				ItemRank,
				AuthorRank,
				EditionRank,
				SubjectRank,
				[Rank]
		FROM	#tmpSortable s INNER JOIN dbo.Item i WITH (NOLOCK)
					ON s.ItemID = i.ItemID
				INNER JOIN dbo.Title t WITH (NOLOCK)
					ON s.TitleID = t.TitleID
		ORDER BY
				ISNULL(NULLIF(i.Year, ''), CONVERT(nvarchar(20), ISNULL(t.StartYear, ''))),
				s.SortTitle,
				s.ItemSequence
	END
	ELSE
	BEGIN
		IF (@SortBy = 'Title')
		BEGIN
			SELECT TOP (@ReturnCount)
					TitleID,
					ItemID,
					FullTitle,
					PartNumber,
					PartName,
					EditionStatement,
					PublicationDetails,
					Datafield_260_a,
					Datafield_260_b,
					Datafield_260_c,
					Volume,
					ExternalUrl,
					Authors,
					Collections,
					Associations,
					InstitutionName,
					TitleRank,
					ItemRank,
					AuthorRank,
					EditionRank,
					SubjectRank,
					[Rank]
			FROM	#tmpSortable		
			ORDER BY
					SortTitle,
					ItemSequence
		END
		ELSE
			BEGIN
					SELECT TOP (@ReturnCount)
					TitleID,
					ItemID,
					FullTitle,
					PartNumber,
					PartName,
					EditionStatement,
					PublicationDetails,
					Datafield_260_a,
					Datafield_260_b,
					Datafield_260_c,
					Volume,
					ExternalUrl,
					Authors,
					Collections,
					Associations,
					InstitutionName,
					TitleRank,
					ItemRank,
					AuthorRank,
					EditionRank,
					SubjectRank,
					[Rank]
			FROM	#tmpSortable		
			ORDER BY
					[Rank] DESC,
					SortTitle,
					ItemSequence
		END
	END
END

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SearchBook. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

END

