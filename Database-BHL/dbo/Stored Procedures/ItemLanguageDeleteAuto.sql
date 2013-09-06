
-- ItemLanguageDeleteAuto PROCEDURE
-- Generated 2/4/2011 12:08:43 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for ItemLanguage

CREATE PROCEDURE ItemLanguageDeleteAuto

@ItemLanguageID INT

AS 

DELETE FROM [dbo].[ItemLanguage]

WHERE

	[ItemLanguageID] = @ItemLanguageID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemLanguageDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

