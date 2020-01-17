CREATE PROCEDURE [dbo].[BibliographicLevelSelectAll]
AS 

SET NOCOUNT ON

SELECT	[BibliographicLevelID],
		[BibliographicLevelName],
		[BibliographicLevelLabel],
		[MARCCode]
FROM	[dbo].[BibliographicLevel]
ORDER BY 
		[BibliographicLevelName]

