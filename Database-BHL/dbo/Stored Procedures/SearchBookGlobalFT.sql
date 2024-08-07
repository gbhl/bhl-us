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
	exec dbo.SearchBook @SearchText, '', '', '', null, '', '', null, @ReturnCount, @SortBy
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
FROM	dbo.Item i
WHERE	ItemStatusID = 40

-- Get initial list of Titles
SELECT	t.TitleID, 
		it.ItemSequence,
		i.ItemID,
		b.BookID
INTO	#tmpActiveTitle
FROM	dbo.Title t
		INNER JOIN dbo.ItemTitle it ON t.TitleID = it.TitleID
		INNER JOIN #tmpActiveItem i ON it.ItemID = i.ItemID
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
WHERE	t.PublishReady = 1

-- Filter to titles/items that match the search text
SELECT	tmp.TitleID, 
		tmp.ItemSequence,
		tmp.ItemID,
		tmp.BookID,
		x.[RANK]
INTO	#tmpTitle
FROM	CONTAINSTABLE(SearchCatalog, (SearchText), @Search) x
		INNER JOIN SearchCatalog c ON c.SearchCatalogID = x.[KEY]
		INNER JOIN #tmpActiveTitle tmp ON c.TitleID = tmp.TitleID AND c.ItemID = tmp.BookID
UNION
SELECT TitleID, ItemSequence, ItemID, BookID, 0 FROM #tmpActiveTitle WHERE @Search = '"**"'

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
-- Compile the final sortable result set.  In doing so, see if any items appear more 
-- than once.  If any do, only show the primary title for those items.
SELECT	tmp.TitleID,
		pt.TitleID AS PrimaryTitleID,
		i.ItemID,
		b.BookID,
		it.ItemSequence,
		t.FullTitle,
		t.SortTitle,
		t.PartNumber,
		t.PartName,
		t.EditionStatement,
		t.PublicationDetails,
		t.Datafield_260_a,
		t.Datafield_260_b,
		t.Datafield_260_c,
		b.Volume,
		b.ExternalUrl,
		c.Authors,
		dbo.fnCollectionStringForTitleAndItem(tmp.TitleID, i.ItemID) AS Collections,
		dbo.fnSeriesStringForTitle (tmp.TitleID) AS Associations,
		c.TitleContributors AS InstitutionName,
		c.HasLocalContent,
		tmp.[Rank]
INTO	#tmpMayContainDups
FROM	#tmpTitleFinal tmp 
		INNER JOIN dbo.Title t ON tmp.TitleID = t.TitleID
		INNER JOIN dbo.ItemTitle it ON t.TitleID = it.TitleID AND it.ItemSequence = tmp.ItemSequence
		INNER JOIN dbo.Item i ON it.ItemID = i.ItemID AND i.ItemStatusID = 40
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
		INNER JOIN dbo.vwItemPrimaryTitle pt ON i.ItemID = pt.ItemID
		INNER JOIN dbo.SearchCatalog c ON t.TitleID = c.TitleID AND b.BookID = c.ItemID

-- Find any duplicated titles
SELECT	TitleID, MIN(ItemID) AS ItemID
INTO	#tmpDups
FROM	#tmpMayContainDups
GROUP BY TitleID HAVING COUNT(*) > 1

-- Show all non-duplicate title information
SELECT	m.TitleID, m.PrimaryTitleID, m.ItemID, m.BookID, m.ItemSequence, m.FullTitle, m.SortTitle, m.PartNumber, m.PartName,
		m.EditionStatement, m.PublicationDetails, m.Datafield_260_a, m.Datafield_260_b, m.Datafield_260_c, m.Volume,
		m.ExternalUrl, m.Authors, m.Collections, m.Associations, m.InstitutionName, m.HasLocalContent, 
		CONVERT(decimal(7, 2), m.[Rank]) AS [Rank]
INTO	#tmpSortable
FROM	#tmpMayContainDups m LEFT JOIN #tmpDups d
			ON m.TitleID = d.TitleID AND m.ItemID <> d.ItemID
WHERE	d.ItemID IS NULL

-- De-emphasize ranking of any items:
--	1) Contributed by Canadiana.org
--	2) Without local content
UPDATE	#tmpSortable
SET		[Rank] = [Rank] / 100.00
FROM	#tmpSortable t
		INNER JOIN dbo.ItemInstitution ii ON t.ItemID = ii.ItemID
		INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
WHERE	(InstitutionCode = 'CANADIANA'
OR		HasLocalContent = 0)
AND		r.InstitutionRoleName = 'Holding Institution'

----------------------------------------------------------
-- Return the sorted result set
IF (@SortBy = 'Author')
BEGIN
	SELECT TOP (@ReturnCount)
			TitleID,
			BookID AS ItemID,
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
				s.BookID AS ItemID,
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
		FROM	#tmpSortable s 
				INNER JOIN dbo.Item i ON s.ItemID = i.ItemID
				INNER JOIN dbo.Title t ON s.TitleID = t.TitleID
				INNER JOIN dbo.Book b ON s.BookID = b.BookID
		ORDER BY
				ISNULL(NULLIF(b.StartYear, ''), CONVERT(nvarchar(20), ISNULL(t.StartYear, ''))),
				s.SortTitle,
				s.ItemSequence
	END
	ELSE
	BEGIN		
		IF (@SortBy = 'Title')
		BEGIN
			SELECT TOP (@ReturnCount)
					TitleID,
					BookID AS ItemID,
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
					BookID AS ItemID,
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

GO
