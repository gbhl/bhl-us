CREATE PROCEDURE [dbo].[TitleLanguageSelectByTitleID]

@TitleID INT

AS 

SET NOCOUNT ON

SELECT	tl.TitleLanguageID,
		tl.TitleID,
		tl.LanguageCode,
		l.LanguageName,
		tl.CreationDate
FROM	dbo.TitleLanguage tl INNER JOIN dbo.Language l
			ON tl.LanguageCode = l.LanguageCode
WHERE	tl.TitleID = @TitleID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleLanguageSelectByTitleID. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
