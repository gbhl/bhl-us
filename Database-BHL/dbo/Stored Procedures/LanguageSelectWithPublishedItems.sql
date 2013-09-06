
CREATE PROCEDURE [dbo].[LanguageSelectWithPublishedItems]

AS

SET NOCOUNT ON

BEGIN

SELECT 	l.LanguageCode,
		l.LanguageName
FROM	dbo.[Language] l INNER JOIN dbo.Item i
			ON l.LanguageCode = i.LanguageCode
WHERE	i.ItemStatusID = 40
AND		LanguageName <> ''
UNION
SELECT	l.LanguageCode,
		l.LanguageName
FROM	dbo.Language l INNER JOIN dbo.ItemLanguage il
			ON l.LanguageCode = il.LanguageCode
		INNER JOIN dbo.Item i
			ON il.ItemID = i.ItemID
WHERE	i.ItemStatusID = 40
AND		l.LanguageName <> ''
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
