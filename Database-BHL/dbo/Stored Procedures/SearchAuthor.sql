
CREATE PROCEDURE [dbo].[SearchAuthor]

@AuthorName nvarchar(300),
@LanguageCode nvarchar(10) = '',
@ReturnCount int = 100

AS 

BEGIN

SET NOCOUNT ON

-- NOTE:  @LanguageCode is no longer used (Feb 12, 2013)

-- Revert to 'normal' SQL search of the search catalog is offline
DECLARE @CatalogStatus int
exec @CatalogStatus = dbo.SearchCatalogCheckStatus
IF (@CatalogStatus = 0)
BEGIN
	exec dbo.AuthorSelectByNameLike @AuthorName, @LanguageCode, @ReturnCount
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
		INNER JOIN dbo.SegmentAuthor sa ON t.CreatorID = sa.AuthorID
		INNER JOIN dbo.Segment s ON sa.SegmentID = s.SegmentID
		INNER JOIN dbo.Author a ON sa.AuthorID = a.AuthorID
		INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID
WHERE	s.SegmentStatusID IN (10, 20)
AND		a.IsActive = 1

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


