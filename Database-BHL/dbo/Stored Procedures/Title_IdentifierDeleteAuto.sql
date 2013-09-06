
-- Title_IdentifierDeleteAuto PROCEDURE
-- Generated 5/1/2012 2:41:41 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for Title_Identifier

CREATE PROCEDURE Title_IdentifierDeleteAuto

@TitleIdentifierID INT

AS 

DELETE FROM [dbo].[Title_Identifier]

WHERE

	[TitleIdentifierID] = @TitleIdentifierID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure Title_IdentifierDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END


