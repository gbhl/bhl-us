
CREATE FUNCTION [dbo].[fnAuthorStringForTitle] 
(
	@TitleID int
)
RETURNS nvarchar(1024)
AS 

BEGIN
	DECLARE @AuthorString nvarchar(max)

	SELECT @AuthorString = STUFF((
		SELECT	'|' + RTRIM(n.FullName + ' ' + 
					a.Numeration + ' ' + a.Unit + ' ' +
					a.Title + ' ' + a.Location + ' ' + 
					n.FullerForm + ' ' + a.StartDate + 
					CASE WHEN a.StartDate <> '' THEN '-' ELSE '' END + a.EndDate)
		FROM	Title t
				INNER JOIN TitleAuthor ta ON t.TitleID = ta.TitleID
				INNER JOIN Author a ON ta.AuthorID = a.AuthorID
				INNER JOIN AuthorRole r ON ta.AuthorRoleID = r.AuthorRoleID
				INNER JOIN AuthorName n ON a.AuthorID = n.AuthorID
		WHERE	t.TitleID = @TitleID
		AND		a.IsActive = 1
		AND		n.IsPreferredName = 1
		ORDER BY r.MarcDataFieldTag, n.FullName ASC
		FOR XML PATH('')
		),1,1,'')

	RETURN SUBSTRING(LTRIM(RTRIM(COALESCE(@AuthorString, ''))), 1, 1024)
END

