CREATE FUNCTION [dbo].[fnAuthorFacetStringForTitle] 
(
	@TitleID int
)
RETURNS nvarchar(MAX)
AS 

BEGIN
	DECLARE @AuthorString nvarchar(MAX)
	DECLARE @CurrentRecord int
	SET @CurrentRecord = 1

	DECLARE @Person int
	SELECT @Person = AuthorTypeID FROM dbo.AuthorType WHERE AuthorTypeName = 'Person'
	
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
			AND		n.IsPreferredName = 1		-- Only preferred names
			AND		a.AuthorTypeID = @Person	-- Only person names used for facets
			AND		a.IsActive = 1
			GROUP BY n.FullName, n.FullerForm, a.Title, a.Unit, a.Location
			) x
	ORDER BY x.MarcDataFieldTag, x.FullName ASC

	RETURN LTRIM(RTRIM(COALESCE(@AuthorString, '')))
END
