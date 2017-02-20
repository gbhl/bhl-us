CREATE FUNCTION [dbo].[fnAuthorSearchStringForTitle] 
(
	@TitleID int
)
RETURNS nvarchar(MAX)
AS 

BEGIN
	DECLARE @AuthorString nvarchar(MAX)
	DECLARE @CurrentRecord int
	SET @CurrentRecord = 1

	SELECT	@AuthorString = COALESCE(@AuthorString, '') +
					(CASE WHEN @CurrentRecord = 1 THEN '' ELSE '|' END) +  
					LTRIM(RTRIM(x.FullName + ' ' +
					ISNULL(NULLIF(x.FullerForm + ' ', ' ' ), '') +
					ISNULL(NULLIF(x.Title + ' ', ' '), '') + 
					ISNULL(NULLIF(x.Unit + ' ', ' '), '') +
					ISNULL(NULLIF(x.Location + ' ', ' '), ''))),
			@CurrentRecord = @CurrentRecord + 1
	FROM	(
			SELECT	MIN(r.MarcDataFieldTag) AS MarcDataFieldTag, n.FullName, 
					n.FullerForm, a.Title, a.Unit, a.Location
			FROM	Title t INNER JOIN TitleAuthor ta ON t.TitleID = ta.TitleID
					INNER JOIN Author a ON ta.AuthorID = a.AuthorID
					INNER JOIN AuthorRole r ON ta.AuthorRoleID = r.AuthorRoleID
					INNER JOIN AuthorName n ON a.AuthorID = n.AuthorID
			WHERE	t.TitleID = @TitleID
			AND		a.IsActive = 1
			AND		n.IsPreferredName = 1	-- Remove this to include all forms of a name
			GROUP BY n.FullName, n.FullerForm, a.Title, a.Unit, a.Location
			) x
	ORDER BY x.MarcDataFieldTag, x.FullName ASC

	RETURN LTRIM(RTRIM(COALESCE(@AuthorString, '')))
END
