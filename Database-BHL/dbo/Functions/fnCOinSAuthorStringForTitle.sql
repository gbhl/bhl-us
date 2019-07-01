CREATE FUNCTION [dbo].[fnCOinSAuthorStringForTitle] 
(
	@TitleID int,
	@IsDC bit
)
RETURNS nvarchar(MAX)
AS 

BEGIN
	DECLARE @AuthorString nvarchar(MAX)
	DECLARE @CurrentRecord int
	SET @CurrentRecord = 1

	SELECT	@AuthorString = COALESCE(@AuthorString, '') +
					(CASE WHEN @CurrentRecord = 1 THEN '' ELSE '|' END) +  x.FullName,
			@CurrentRecord = @CurrentRecord + 1
	FROM	(
			SELECT	MIN(r.MarcDataFieldTag) AS MarcDataFieldTag, n.FullName, ta.SequenceOrder
			FROM	Title t INNER JOIN TitleAuthor ta ON t.TitleID = ta.TitleID
					INNER JOIN Author a ON ta.AuthorID = a.AuthorID
					INNER JOIN AuthorRole r ON ta.AuthorRoleID = r.AuthorRoleID
					INNER JOIN AuthorName n ON a.AuthorID = n.AuthorID
			WHERE	t.TitleID = @TitleID
			AND		a.IsActive = 1
			AND		n.IsPreferredName = 1
			GROUP BY n.FullName, ta.SequenceOrder
			) x
	ORDER BY x.SequenceOrder, x.MarcDataFieldTag, x.FullName ASC

	RETURN LTRIM(RTRIM(COALESCE(@AuthorString, '')))
END
