
-- LanguageDeleteAuto PROCEDURE
-- Generated 1/18/2008 11:10:47 AM
-- Do not modify the contents of this procedure.
-- Delete Procedure for Language

CREATE PROCEDURE LanguageDeleteAuto

@LanguageCode NVARCHAR(10) /* Code for a language. */

AS 

DELETE FROM [dbo].[Language]

WHERE

	[LanguageCode] = @LanguageCode

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure LanguageDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

