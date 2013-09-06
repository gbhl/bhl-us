
-- LanguageSelectAuto PROCEDURE
-- Generated 1/18/2008 11:10:47 AM
-- Do not modify the contents of this procedure.
-- Select Procedure for Language

CREATE PROCEDURE LanguageSelectAuto

@LanguageCode NVARCHAR(10) /* Code for a language. */

AS 

SET NOCOUNT ON

SELECT 

	[LanguageCode],
	[LanguageName],
	[Note]

FROM [dbo].[Language]

WHERE
	[LanguageCode] = @LanguageCode

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure LanguageSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

