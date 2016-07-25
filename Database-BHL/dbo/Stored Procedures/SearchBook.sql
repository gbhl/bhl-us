CREATE PROCEDURE [dbo].[SearchBook]

@Title			nvarchar(2000) = '',
@AuthorLastName	nvarchar(255) = '',
@Volume			nvarchar(100) = '',
@Edition		nvarchar(450) = '',
@Year			smallint = NULL,
@Subject		nvarchar(50) = '',
@LanguageCode	nvarchar(10) = '',
@CollectionID	int = null,
@ReturnCount	int = 100,
@SortBy			nvarchar(50) = 'Title'

AS 

SET NOCOUNT ON

-- NOTE: The following queries have been broken down into multiple steps to 
-- best take advantage of the table indexes

-- Get initial list, filtered by title, language, and volume
SELECT	t.TitleID, 
		ti.ItemSequence,
		i.ItemID
INTO	#tmpTitle
FROM	dbo.Title t WITH (NOLOCK) LEFT JOIN (
				SELECT	ta.* 
				FROM	dbo.TitleAssociation ta WITH (NOLOCK) INNER JOIN dbo.TitleAssociationType tat WITH (NOLOCK)
							ON ta.TitleAssociationTypeID = tat.TitleAssociationTypeID
				WHERE	ta.Active = 1
				AND		tat.TitleAssociationLabel = 'Series') ta
			ON t.TitleID = ta.TitleID		
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON t.TitleID = ti.TitleID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON ti.ItemID = i.ItemID
		LEFT JOIN dbo.TitleLanguage tl WITH (NOLOCK) ON t.TitleID = tl.TitleID AND tl.LanguageCode = @LanguageCode
		LEFT JOIN dbo.ItemLanguage il WITH (NOLOCK) ON i.ItemID = il.ItemID AND il.LanguageCode = @LanguageCode
		LEFT JOIN dbo.TitleKeyword tk WITH (NOLOCK) ON t.TitleID = tk.TitleID
		LEFT JOIN dbo.Keyword k WITH (NOLOCK) ON tk.KeywordID = k.KeywordID
WHERE	(t.FullTitle LIKE '%' + @Title + '%' OR ta.Title LIKE '%' + @Title + '%')
AND		(ISNULL(k.Keyword, '') LIKE @Subject + '%' OR @Subject = '')
AND		t.PublishReady = 1
AND		(t.LanguageCode = @LanguageCode OR
			i.LanguageCode = @LanguageCode OR
			ISNULL(tl.LanguageCode, '') = @LanguageCode OR
			ISNULL(il.LanguageCode, '') = @LanguageCode OR
			@LanguageCode = '')
AND		(i.Volume LIKE '%' + @Volume + '%' OR @Volume = '')
AND		i.ItemStatusID = 40

-- If a volume was specified, then add any items that match everything *except*
-- volume to our initial list.  We want users to know that we have a title, even if
-- we don't have the exact volume that they asked for.  This also helps if the volume
-- data is messy (maybe we DO have the volume, but the initial search didn't find it).
IF (@Volume <> '')
BEGIN
	INSERT INTO #tmpTitle
	SELECT	t.TitleID, 
			ti.ItemSequence,
			i.ItemID
	FROM	dbo.Title t WITH (NOLOCK) LEFT JOIN (
					SELECT	ta.* 
					FROM	dbo.TitleAssociation ta WITH (NOLOCK) INNER JOIN dbo.TitleAssociationType tat WITH (NOLOCK)
								ON ta.TitleAssociationTypeID = tat.TitleAssociationTypeID
					WHERE	ta.Active = 1
					AND		tat.TitleAssociationLabel = 'Series') ta
				ON t.TitleID = ta.TitleID		
			INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON t.TitleID = ti.TitleID
			INNER JOIN dbo.Item i WITH (NOLOCK) ON ti.ItemID = i.ItemID
			LEFT JOIN dbo.TitleLanguage tl WITH (NOLOCK) ON t.TitleID = tl.TitleID AND tl.LanguageCode = @LanguageCode
			LEFT JOIN dbo.ItemLanguage il WITH (NOLOCK) ON i.ItemID = il.ItemID AND il.LanguageCode = @LanguageCode
	WHERE	(t.FullTitle LIKE '%' + @Title + '%' OR ta.Title LIKE '%' + @Title + '%')
	AND		t.PublishReady = 1
	AND		(t.LanguageCode = @LanguageCode OR
				i.LanguageCode = @LanguageCode OR
				ISNULL(tl.LanguageCode, '') = @LanguageCode OR
				ISNULL(il.LanguageCode, '') = @LanguageCode OR
				@LanguageCode = '')
	AND		i.ItemStatusID = 40
	AND		t.TitleID NOT IN (SELECT TitleID FROM #tmpTitle)
END

-- Filter by Year and Edition
SELECT	tmp.TitleID,
		tmp.ItemSequence,
		tmp.ItemID
INTO	#tmpTitleFilter1
FROM	#tmpTitle tmp INNER JOIN dbo.Title t WITH (NOLOCK) ON tmp.TitleID = t.TitleID
WHERE	(t.EditionStatement LIKE '%' + @Edition + '%' OR @Edition = '')
AND		(@Year IS NULL OR 
			ISNULL(t.StartYear, 0) = @Year OR 
			ISNULL(t.EndYear, 0) = @Year OR 
			@Year BETWEEN ISNULL(t.StartYear, 0) and ISNULL(t.EndYear, 0))

-- Filter by author
SELECT DISTINCT
		tmp.TitleID,
		tmp.ItemSequence,
		tmp.ItemID
INTO	#tmpTitleFilter2
FROM	#tmpTitleFilter1 tmp 
		LEFT JOIN dbo.TitleAuthor ta WITH (NOLOCK) ON tmp.TitleID = ta.TitleID
		LEFT JOIN dbo.Author a WITH (NOLOCK) ON ta.AuthorID = a.AuthorID AND a.IsActive = 1
		LEFT JOIN dbo.AuthorName n WITH (NOLOCK) ON a.AuthorID = n.AuthorID
WHERE	ISNULL(n.FullName, '') LIKE @AuthorLastName + '%' 
OR		@AuthorLastName = ''

-- Filter by collection
CREATE TABLE #tmpTitleFinal
	(
	TitleID int NOT NULL,
	ItemSequence smallint NOT NULL
	)

-- Get first item of each title associated with a collection
INSERT	#tmpTitleFinal
SELECT	tmp.TitleID,
		MIN(tmp.ItemSequence) AS ItemSequence
FROM	#tmpTitleFilter2 tmp INNER JOIN dbo.TitleCollection tc WITH (NOLOCK) ON tmp.TitleID = tc.TitleID
		INNER JOIN dbo.Collection c WITH (NOLOCK) ON tc.CollectionID = c.CollectionID
WHERE	tc.CollectionID = @CollectionID
AND		c.Active = 1	-- collection must be active
AND		c.CollectionTarget IN ('All', 'BHL')  -- collection target must include BHL
GROUP BY
		tmp.TitleID

-- Get items directly associated with a collection
INSERT	#tmpTitleFinal
SELECT	tmp.TitleID,
		tmp.ItemSequence
FROM	#tmpTitleFilter2 tmp INNER JOIN dbo.ItemCollection ic WITH (NOLOCK) ON tmp.ItemID = ic.ItemID
		INNER JOIN dbo.Collection c WITH (NOLOCK) ON ic.CollectionID = c.CollectionID
WHERE	ic.CollectionID = @CollectionID
AND		c.Active = 1	-- collection must be active
AND		c.CollectionTarget IN ('All', 'BHL')  -- collection target must include BHL

-- Just get first items of titles when no collection specified
INSERT	#tmpTitleFinal
SELECT	tmp.TitleID,
		MIN(tmp.ItemSequence) AS ItemSequence
FROM	#tmpTitleFilter2 tmp 
WHERE	@CollectionID IS NULL
GROUP BY
		tmp.TitleID

----------------------------------------------------------
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
		c.TitleContributors AS InstitutionName
INTO	#tmpMayContainDups
FROM	#tmpTitleFinal tmp WITH (NOLOCK)
		INNER JOIN dbo.Title t WITH (NOLOCK) ON tmp.TitleID = t.TitleID
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON t.TitleID = ti.TitleID AND ti.ItemSequence = tmp.ItemSequence
		INNER JOIN dbo.Item i WITH (NOLOCK) ON ti.ItemID = i.ItemID AND i.ItemStatusID = 40
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID AND i.ItemID = c.ItemID

-- Find any duplicated items
SELECT	ItemID
INTO	#tmpDups
FROM	#tmpMayContainDups
GROUP BY ItemID HAVING COUNT(*) > 1

-- Show all non-duplicate item information, plus the primary title information for duplicate items
SELECT	m.*
INTO	#tmpSortable
FROM	#tmpMayContainDups m LEFT JOIN #tmpDups d
			ON m.ItemID = d.ItemID
WHERE	d.ItemID IS NULL
UNION
SELECT	m.*
FROM	#tmpMayContainDups m INNER JOIN #tmpDups d
			ON m.ItemID = d.ItemID
WHERE	m.TitleID = PrimaryTitleID

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
			InstitutionName
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
				s.InstitutionName
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
				InstitutionName
		FROM	#tmpSortable		
		ORDER BY
				SortTitle,
				ItemSequence
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
