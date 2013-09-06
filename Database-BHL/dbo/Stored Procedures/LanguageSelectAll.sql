CREATE PROCEDURE [dbo].[LanguageSelectAll]

AS 

SELECT 
	[LanguageCode],
	[LanguageName],
	[Note]
FROM [dbo].[Language]
ORDER BY LanguageName
