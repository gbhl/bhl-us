CREATE FUNCTION [dbo].[fnAuthorSearchStringForAuthor] 
(
	@AuthorID int,
	@Delimiter nvarchar(5)
)
RETURNS nvarchar(2000)
AS 

BEGIN
	DECLARE @AuthorString nvarchar(2000)
	DECLARE @CurrentRecord int
	SET @CurrentRecord = 1

	SELECT	@AuthorString = COALESCE(@AuthorString, '') +
					(CASE WHEN @CurrentRecord = 1 THEN '' ELSE @Delimiter END) +  
					LTRIM(RTRIM(x.FullName + ' ' +
					ISNULL(NULLIF(x.FullerForm + ' ', ' ' ), '') +
					ISNULL(NULLIF(x.Title + ' ', ' '), '') + 
					ISNULL(NULLIF(x.Unit + ' ', ' '), '') +
					ISNULL(NULLIF(x.Location + ' ', ' '), ''))),
			@CurrentRecord = @CurrentRecord + 1
	FROM	(
			SELECT	n.FullName, n.FullerForm, a.Title, a.Unit, a.Location
			FROM	Author a
					INNER JOIN AuthorName n ON a.AuthorID = n.AuthorID
			WHERE	a.AuthorID = @AuthorID
			AND		a.IsActive = 1
			GROUP BY n.FullName, n.FullerForm, a.Title, a.Unit, a.Location
			) x
	ORDER BY x.FullName ASC

	RETURN SUBSTRING(LTRIM(RTRIM(COALESCE(@AuthorString, ''))), 1, 2000)
END
