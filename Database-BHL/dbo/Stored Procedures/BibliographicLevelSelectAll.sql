CREATE PROCEDURE [dbo].[BibliographicLevelSelectAll]
AS 

SET NOCOUNT ON

SELECT	[BibliographicLevelID],
		[BibliographicLevelName],
		[MARCCode]
FROM	[dbo].[BibliographicLevel]
ORDER BY 
		[BibliographicLevelName]

