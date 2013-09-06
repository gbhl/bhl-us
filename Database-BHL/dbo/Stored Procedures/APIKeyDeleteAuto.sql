
-- APIKeyDeleteAuto PROCEDURE
-- Generated 2/4/2010 3:26:40 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for APIKey

CREATE PROCEDURE APIKeyDeleteAuto

@ApiKeyID INT

AS 

DELETE FROM [dbo].[APIKey]

WHERE

	[ApiKeyID] = @ApiKeyID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure APIKeyDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

