
CREATE PROCEDURE [dbo].[SearchBookGlobalFT]

@SearchText		nvarchar(2000) = '',
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
	exec dbo.SearchBook @SearchText, '', '', '', '', '', null, @ReturnCount, @SortBy
	RETURN
END

DECLARE @Search nvarchar(4000)

-- Transform the search terms into full-text search phrases
SELECT @Search = dbo.fnGetFullTextSearchString(@SearchText)

-- NOTE: The following queries have been broken down into multiple steps to 
-- best take advantage of the table indexes

-- Get initial list of items
SELECT	i.ItemID
INTO	#tmpActiveItem
FROM	dbo.Item i WITH (NOLOCK)
WHERE	ItemStatusID = 40

-- Get initial list of Titles
SELECT	t.TitleID, 
		ti.ItemSequence,
		i.ItemID
INTO	#tmpActiveTitle
FROM	dbo.Title t WITH (NOLOCK)
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON t.TitleID = ti.TitleID
		INNER JOIN #tmpActiveItem i ON ti.ItemID = i.ItemID
WHERE	t.PublishReady = 1

-- Filter to titles/items that match the search text
SELECT	tmp.TitleID, 
		tmp.ItemSequence,
		tmp.ItemID,
		x.[RANK]
INTO	#tmpTitle
FROM	CONTAINSTABLE(SearchCatalog, (SearchText), @Search) x
		INNER JOIN SearchCatalog c WITH (NOLOCK) ON c.SearchCatalogID = x.[KEY]
		INNER JOIN #tmpActiveTitle tmp ON c.TitleID = tmp.TitleID AND c.ItemID = tmp.ItemID
UNION
SELECT TitleID, ItemSequence, ItemID, 0 FROM #tmpActiveTitle WHERE @Search = '"**"'

-- Show only first items of titles
SELECT	tmp.TitleID,
		MIN(tmp.ItemSequence) AS ItemSequence,
		MAX(tmp.[RANK]) AS [Rank]
INTO	#tmpTitleFinal
FROM	#tmpTitle tmp 
GROUP BY
		tmp.TitleID

-- Get the correct RANK value for the item
UPDATE	#tmpTitleFinal
SET		[Rank] = t.[RANK]
FROM	#tmpTitleFinal f INNER JOIN #tmpTitle t
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
		tmp.[Rank]
INTO	#tmpMayContainDups
FROM	#tmpTitleFinal tmp 
		INNER JOIN dbo.Title t WITH (NOLOCK) ON tmp.TitleID = t.TitleID
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON t.TitleID = ti.TitleID AND ti.ItemSequence = tmp.ItemSequence
		INNER JOIN dbo.Item i WITH (NOLOCK) ON ti.ItemID = i.ItemID AND i.ItemStatusID = 40
		LEFT JOIN dbo.Institution inst WITH (NOLOCK) ON i.InstitutionCode = inst.InstitutionCode
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK)ON t.TitleID = c.TitleID AND i.ItemID = c.ItemID

-- Find any duplicated items
SELECT	ItemID
INTO	#tmpDups
FROM	#tmpMayContainDups
GROUP BY ItemID HAVING COUNT(*) > 1

-- Show all non-duplicate item information, plus the primary title information for duplicate items
SELECT	m.TitleID, m.PrimaryTitleID, m.ItemID, m.ItemSequence, m.FullTitle, m.SortTitle, m.PartNumber, m.PartName,
		m.EditionStatement, m.PublicationDetails, m.Datafield_260_a, m.Datafield_260_b, m.Datafield_260_c, m.Volume,
		m.ExternalUrl, m.Authors, m.Collections, m.Associations, m.InstitutionName, CONVERT(decimal(7, 2), 
		m.[Rank]) AS [Rank]
INTO	#tmpSortable
FROM	#tmpMayContainDups m LEFT JOIN #tmpDups d
			ON m.ItemID = d.ItemID
WHERE	d.ItemID IS NULL
UNION
SELECT	m.TitleID, m.PrimaryTitleID, m.ItemID, m.ItemSequence, m.FullTitle, m.SortTitle, m.PartNumber, m.PartName,
		m.EditionStatement, m.PublicationDetails, m.Datafield_260_a, m.Datafield_260_b, m.Datafield_260_c, m.Volume,
		m.ExternalUrl, m.Authors, m.Collections, m.Associations, m.InstitutionName, CONVERT(decimal(7, 2), 
		m.[Rank]) AS [Rank]
FROM	#tmpMayContainDups m INNER JOIN #tmpDups d
			ON m.ItemID = d.ItemID
WHERE	m.TitleID = PrimaryTitleID


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
				s.[Rank]
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
	RAISERROR('An error occurred in procedure SearchBookGlobalFT. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

END



