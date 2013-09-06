CREATE PROCEDURE [dbo].[KeywordSelectKeywordByTitleID]
	@TitleID int
AS 

SET NOCOUNT ON

SELECT DISTINCT Keyword
FROM	dbo.TitleKeywordView
WHERE	TitleID = @TitleID
ORDER BY Keyword



