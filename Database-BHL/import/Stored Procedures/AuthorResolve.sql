CREATE PROCEDURE [import].[AuthorResolve]

@FullName nvarchar(300),
@LastName nvarchar(150) = '',
@FirstName nvarchar(150) = '',
@StartDate nvarchar(25) = '',
@EndDate nvarchar(25) = ''

AS

BEGIN

SET NOCOUNT ON

-- Not an ideal query for matching author name variants, but the full-text alternative 
-- produces too many false positives (too many hits instead of not enough).
SELECT	a.AuthorID
INTO	#Author
FROM	dbo.Author a
		INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID
-- Ignore periods and commas in author names
WHERE	(REPLACE(REPLACE(n.FullName,',',''),'.','') = REPLACE(REPLACE(@FullName,',',''),'.','') OR
		 REPLACE(REPLACE(dbo.fnReverseAuthorName(n.FullName),',',''),'.','') = REPLACE(REPLACE(@FullName,',',''),'.','') OR
		(n.LastName = @LastName AND n.FirstName = @FirstName AND ISNULL(@LastName, '') <> '' AND ISNULL(@FirstName, '') <> ''))
AND		(a.StartDate = @StartDate OR ISNULL(@StartDate, '') = '')
AND		(a.EndDate = @EndDate OR ISNULL(@EndDate, '') = '')
AND		a.IsActive = 1

/*
-- PRODUCES TOO MANY FALSE POSITIVES, INSTEAD OF A SINGLE UNIQUE HIT

DECLARE @SearchCondition nvarchar(4000)

-- Transform the search term into a full-text search phrase
SELECT @SearchCondition = 
	dbo.fnGetFullTextSearchString(RTRIM(@FullName + ' ' + ISNULL(@LastName, '') + ' ' + ISNULL(@FirstName, '')))

-- Get the matching authors
SELECT	c.CreatorID AS AuthorID
INTO	#Author
FROM	CONTAINSTABLE(SearchCatalogCreator, CreatorName, @SearchCondition) x
		INNER JOIN dbo.SearchCatalogCreator c ON c.SearchCatalogCreatorID = x.[KEY]
		INNER JOIN dbo.Author a ON c.CreatorID = a.AuthorID
		INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID
WHERE	(a.StartDate = @StartDate OR ISNULL(@StartDate, '') = '')
AND		(a.EndDate = @EndDate OR ISNULL(@EndDate, '') = '')
*/

-- If the selected author has been redirected to a different author, 
-- then use that author instead.  Follow the "redirect" chain up to
-- ten levels.
SELECT	AuthorID = COALESCE(a10.AuthorID, a9.AuthorID, a8.AuthorID, a7.AuthorID, a6.AuthorID,
							a5.AuthorID, a4.AuthorID, a3.AuthorID, a2.AuthorID, a1.AuthorID)
FROM	#Author a 
		INNER JOIN dbo.Author a1 ON a.AuthorID = a1.AuthorID
		LEFT JOIN dbo.Author a2 ON a1.RedirectAuthorID = a2.AuthorID
		LEFT JOIN dbo.Author a3 ON a2.RedirectAuthorID = a3.AuthorID
		LEFT JOIN dbo.Author a4 ON a3.RedirectAuthorID = a4.AuthorID
		LEFT JOIN dbo.Author a5 ON a4.RedirectAuthorID = a5.AuthorID
		LEFT JOIN dbo.Author a6 ON a5.RedirectAuthorID = a6.AuthorID
		LEFT JOIN dbo.Author a7 ON a6.RedirectAuthorID = a7.AuthorID
		LEFT JOIN dbo.Author a8 ON a7.RedirectAuthorID = a8.AuthorID
		LEFT JOIN dbo.Author a9 ON a8.RedirectAuthorID = a9.AuthorID
		LEFT JOIN dbo.Author a10 ON a9.RedirectAuthorID = a10.AuthorID

END
