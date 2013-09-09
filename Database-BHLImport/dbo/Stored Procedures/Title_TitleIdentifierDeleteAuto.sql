
-- Title_TitleIdentifierDeleteAuto PROCEDURE
-- Generated 9/4/2008 2:16:32 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for Title_TitleIdentifier

CREATE PROCEDURE Title_TitleIdentifierDeleteAuto

@Title_TitleIdentifierID INT

AS 

DELETE FROM [dbo].[Title_TitleIdentifier]

WHERE

	[Title_TitleIdentifierID] = @Title_TitleIdentifierID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure Title_TitleIdentifierDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

