
-- LanguageInsertAuto PROCEDURE
-- Generated 1/18/2008 11:10:47 AM
-- Do not modify the contents of this procedure.
-- Insert Procedure for Language

CREATE PROCEDURE LanguageInsertAuto

@LanguageCode NVARCHAR(10) /* Code for a language. */,
@LanguageName NVARCHAR(20) /* Name used for the language. */,
@Note NVARCHAR(255) = null /* Notes about this Language and its use. */

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Language]
(
	[LanguageCode],
	[LanguageName],
	[Note]
)
VALUES
(
	@LanguageCode,
	@LanguageName,
	@Note
)

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure LanguageInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

