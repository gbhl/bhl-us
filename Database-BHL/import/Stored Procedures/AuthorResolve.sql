CREATE PROCEDURE [import].[AuthorResolve]

@FullName nvarchar(300),
@LastName nvarchar(150) = '',
@FirstName nvarchar(150) = '',
@StartDate nvarchar(25) = '',
@EndDate nvarchar(25) = '',
@AuthorID int = null

AS

BEGIN

SET NOCOUNT ON

CREATE TABLE #Author (AuthorID int NOT NULL)

IF (@AuthorID IS NULL)
BEGIN
	DECLARE @NumHits int

	-- Not an ideal query for matching author name variants, but the full-text alternative 
	-- produces too many false positives (too many hits instead of not enough).
	INSERT	#Author
	SELECT	a.AuthorID
	FROM	dbo.Author a
			INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID
	-- Ignore periods and commas in author names
	WHERE	(REPLACE(REPLACE(n.FullName,',',''),'.','') = REPLACE(REPLACE(@FullName,',',''),'.','') OR
			 REPLACE(REPLACE(dbo.fnReverseAuthorName(n.FullName),',',''),'.','') = REPLACE(REPLACE(@FullName,',',''),'.','') OR
			(n.LastName = @LastName AND n.FirstName = @FirstName AND ISNULL(@LastName, '') <> '' AND ISNULL(@FirstName, '') <> ''))
	AND		(dbo.fnRemoveNonNumericCharacters(a.StartDate) = dbo.fnRemoveNonNumericCharacters(@StartDate) OR ISNULL(@StartDate, '') = '')
	AND		(dbo.fnRemoveNonNumericCharacters(a.EndDate) = dbo.fnRemoveNonNumericCharacters(@EndDate) OR ISNULL(@EndDate, '') = '')
	AND		a.IsActive = 1

	SELECT @NumHits = @@ROWCOUNT

	IF (@NumHits = 0)
	BEGIN
		-- No active authors found, try inactive authors instead
		INSERT	#Author
		SELECT	a.AuthorID
		FROM	dbo.Author a
				INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID
		-- Ignore periods and commas in author names
		WHERE	(REPLACE(REPLACE(n.FullName,',',''),'.','') = REPLACE(REPLACE(@FullName,',',''),'.','') OR
				 REPLACE(REPLACE(dbo.fnReverseAuthorName(n.FullName),',',''),'.','') = REPLACE(REPLACE(@FullName,',',''),'.','') OR
				(n.LastName = @LastName AND n.FirstName = @FirstName AND ISNULL(@LastName, '') <> '' AND ISNULL(@FirstName, '') <> ''))
		AND		(dbo.fnRemoveNonNumericCharacters(a.StartDate) = dbo.fnRemoveNonNumericCharacters(@StartDate) OR ISNULL(@StartDate, '') = '')
		AND		(dbo.fnRemoveNonNumericCharacters(a.EndDate) = dbo.fnRemoveNonNumericCharacters(@EndDate) OR ISNULL(@EndDate, '') = '')
		AND		a.IsActive = 0
	END
END
ELSE
BEGIN
	INSERT	#Author
	SELECT	AuthorID
	FROM	dbo.Author
	WHERE	AuthorID = @AuthorID
END

-- If the selected author has been redirected to a different author, 
-- then use that author instead.  Follow the "redirect" chain up to
-- ten levels.
SELECT	AuthorID = COALESCE(a10.AuthorID, a9.AuthorID, a8.AuthorID, a7.AuthorID, a6.AuthorID,
							a5.AuthorID, a4.AuthorID, a3.AuthorID, a2.AuthorID, a1.AuthorID)
INTO	#Final
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

-- Only return "active" author IDs
SELECT f.AuthorID FROM #Final f INNER JOIN dbo.Author a ON f.AuthorID = a.AuthorID WHERE a.IsActive = 1

END
