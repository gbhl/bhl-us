
-- LanguageUpdateAuto PROCEDURE
-- Generated 1/18/2008 11:10:47 AM
-- Do not modify the contents of this procedure.
-- Update Procedure for Language

CREATE PROCEDURE LanguageUpdateAuto

@LanguageCode NVARCHAR(10) /* Code for a language. */,
@LanguageName NVARCHAR(20) /* Name used for the language. */,
@Note NVARCHAR(255) /* Notes about this Language and its use. */

AS 

SET NOCOUNT ON

UPDATE [dbo].[Language]

SET

	[LanguageCode] = @LanguageCode,
	[LanguageName] = @LanguageName,
	[Note] = @Note

WHERE
	[LanguageCode] = @LanguageCode
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure LanguageUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[LanguageCode],
		[LanguageName],
		[Note]

	FROM [dbo].[Language]
	
	WHERE
		[LanguageCode] = @LanguageCode
	
	RETURN -- update successful
END

