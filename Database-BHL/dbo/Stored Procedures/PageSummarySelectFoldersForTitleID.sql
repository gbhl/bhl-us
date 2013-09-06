
CREATE PROCEDURE [dbo].[PageSummarySelectFoldersForTitleID]

@TitleID int

AS 
BEGIN

SET NOCOUNT ON

SELECT DISTINCT 
		OCRFolderShare, 
		FileRootFolder 
FROM	PageSummaryView 
WHERE	TitleID = @TitleID

END
