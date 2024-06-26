SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SearchAuthor]

@AuthorName nvarchar(300),
@ReturnCount int = 100

AS 

BEGIN

SET NOCOUNT ON

-- Revert to 'normal' SQL search of the search catalog is offline
DECLARE @CatalogStatus int
exec @CatalogStatus = dbo.SearchCatalogCheckStatus
IF (@CatalogStatus = 0)
BEGIN
	exec dbo.AuthorSelectByNameLike @AuthorName, @ReturnCount
	RETURN
END

DECLARE @SearchCondition nvarchar(4000)

-- Transform the search term into a full-text search phrase
SELECT @SearchCondition = dbo.fnGetFullTextSearchString(@AuthorName)

-- Get the matching authors
SELECT	c.CreatorID
INTO	#tmpAuthor
FROM	CONTAINSTABLE(SearchCatalogCreator, CreatorName, @SearchCondition) x
		INNER JOIN SearchCatalogCreator c ON c.SearchCatalogCreatorID = x.[KEY]
		
SELECT	v.AuthorID,
		v.FullName,
		v.Numeration,
		v.Unit,
		v.Title,
		v.Location,
		v.StartDate + CASE WHEN v.StartDate <> '' THEN '-' ELSE '' END + v.EndDate AS Dates,
		v.FullerForm,
		v.StartDate,
		v.EndDate,
		v.IsActive
INTO	#tmpFinal
FROM	#tmpAuthor t
		INNER JOIN dbo.TitleAuthorView v ON t.CreatorID = v.AuthorID
WHERE	v.PublishReady = 1
AND		v.IsActive = 1
AND		v.IsPreferredName = 1

UNION

SELECT	a.AuthorID, 
		n.FullName, 
		a.Numeration, 
		a.Unit, 
		a.Title, 
		a.Location,
		a.StartDate + CASE WHEN a.StartDate <> '' THEN '-' ELSE '' END + a.EndDate AS Dates,
		n.FullerForm, 
		a.StartDate,
		a.EndDate,
		a.IsActive
FROM	#tmpAuthor t
		INNER JOIN dbo.ItemAuthor ia ON t.CreatorID = ia.AuthorID
		INNER JOIN dbo.Item i ON ia.ItemID = i.ItemID
		INNER JOIN dbo.Segment s ON i.ItemID = s.ItemID
		INNER JOIN dbo.Author a ON ia.AuthorID = a.AuthorID
		INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID
WHERE	i.ItemStatusID IN (30, 40)
AND		a.IsActive = 1
AND		n.IsPreferredName = 1

SELECT TOP (@ReturnCount)
		AuthorID, 
		FullName, 
		Numeration, 
		Unit, 
		Title, 
		Location,
		Dates,
		FullerForm, 
		StartDate,
		EndDate,
		IsActive
FROM	#tmpFinal
ORDER BY 
		FullName, Numeration, Unit, Title, 
		Location, StartDate, EndDate, FullerForm

END


GO
