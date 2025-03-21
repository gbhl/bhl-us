﻿
-- TitleLanguageSelectAuto PROCEDURE
-- Generated 2/4/2011 12:08:43 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for TitleLanguage

CREATE PROCEDURE TitleLanguageSelectAuto

@TitleLanguageID INT

AS 

SET NOCOUNT ON

SELECT 

	[TitleLanguageID],
	[TitleID],
	[LanguageCode],
	[CreationDate],
	[CreationUserID]

FROM [dbo].[TitleLanguage]

WHERE
	[TitleLanguageID] = @TitleLanguageID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleLanguageSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

