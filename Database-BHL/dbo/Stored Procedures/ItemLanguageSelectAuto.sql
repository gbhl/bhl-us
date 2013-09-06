﻿
-- ItemLanguageSelectAuto PROCEDURE
-- Generated 2/4/2011 12:08:43 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for ItemLanguage

CREATE PROCEDURE ItemLanguageSelectAuto

@ItemLanguageID INT

AS 

SET NOCOUNT ON

SELECT 

	[ItemLanguageID],
	[ItemID],
	[LanguageCode],
	[CreationDate],
	[CreationUserID]

FROM [dbo].[ItemLanguage]

WHERE
	[ItemLanguageID] = @ItemLanguageID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemLanguageSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

