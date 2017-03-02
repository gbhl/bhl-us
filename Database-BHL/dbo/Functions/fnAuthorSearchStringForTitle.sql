CREATE FUNCTION [dbo].[fnAuthorSearchStringForTitle] 
(
	@TitleID int,
	@PreferredOnly int
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
			AND		(n.IsPreferredName = @PreferredOnly OR @PreferredOnly = 0)
					 -- 0 to include all names associated with the author
			AND		a.IsActive = 1
			GROUP BY n.FullName, n.FullerForm, a.Title, a.Unit, a.Location
			) x
	ORDER BY x.MarcDataFieldTag, x.FullName ASC

	RETURN LTRIM(RTRIM(COALESCE(@AuthorString, '')))
END
