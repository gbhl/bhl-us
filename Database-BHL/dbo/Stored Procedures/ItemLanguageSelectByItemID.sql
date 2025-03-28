﻿CREATE PROCEDURE [dbo].[ItemLanguageSelectByItemID]

@ItemID INT

AS 

SET NOCOUNT ON

SELECT	il.ItemLanguageID,
		il.ItemID,
		il.LanguageCode,
		l.LanguageName,
		il.CreationDate
FROM	dbo.ItemLanguage il INNER JOIN dbo.Language l
			ON il.LanguageCode = l.LanguageCode
WHERE	il.ItemID = @ItemID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemLanguageSelectByItemID. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
