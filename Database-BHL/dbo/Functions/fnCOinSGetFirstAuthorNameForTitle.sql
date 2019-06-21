CREATE FUNCTION [dbo].[fnCOinSGetFirstAuthorNameForTitle] 
(
	@TitleID int,
	@MarcDataFieldTag nvarchar(3)
)
RETURNS nvarchar(255)
AS 

BEGIN
	DECLARE @AuthorName nvarchar(255)
	DECLARE @MarcDataField nvarchar(3)
	SET @AuthorName = NULL

	SELECT TOP 1 @AuthorName = n.FullName, @MarcDataField = r.MarcDataFieldTag
	FROM	dbo.TitleAuthor ta INNER JOIN dbo.Author a ON ta.AuthorID = a.AuthorID
			INNER JOIN dbo.AuthorRole r ON ta.AuthorRoleID = r.AuthorRoleID
			INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID
	WHERE	ta.TitleID = @TitleID
	AND		a.IsActive = 1
	AND		n.IsPreferredName = 1
	ORDER BY ta.SequenceOrder, r.MarcDataFieldTag, n.FullName

	IF (@MarcDataFieldTag = '100' AND @MarcDataField NOT IN ('100', '700')) SET @AuthorName = ''
	IF (@MarcDataFieldTag = '110' AND @MarcDataField NOT IN ('110', '710')) SET @AuthorName = ''

	RETURN LTRIM(RTRIM(COALESCE(@AuthorName, '')))
END
