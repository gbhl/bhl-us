CREATE PROCEDURE [dbo].[LanguageSelectWithPublishedItems]

AS

SET NOCOUNT ON

BEGIN

SELECT 	l.LanguageCode,
		l.LanguageName
INTO	#lang1
FROM	dbo.[Language] l 
		INNER JOIN dbo.Book b ON l.LanguageCode = b.LanguageCode
		INNER JOIN dbo.Item i ON b.ItemID = i.ItemID
WHERE	i.ItemStatusID = 40
AND		LanguageName <> ''

SELECT	l.LanguageCode,
		l.LanguageName
INTO	#lang2
FROM	dbo.Language l 
		INNER JOIN dbo.ItemLanguage il ON l.LanguageCode = il.LanguageCode
		INNER JOIN dbo.Item i ON il.ItemID = i.ItemID
WHERE	i.ItemStatusID = 40
AND		l.LanguageName <> ''

SELECT	LanguageCode,
		LanguageName
FROM	#lang1
UNION
SELECT	LanguageCode,
		LanguageName
FROM	#lang2
ORDER BY 
		LanguageName

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure LanguageSelectWithPublishedItems. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

END
