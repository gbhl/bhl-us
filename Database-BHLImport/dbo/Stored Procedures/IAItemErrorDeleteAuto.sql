
-- IAItemErrorDeleteAuto PROCEDURE
-- Generated 11/18/2009 1:43:59 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for IAItemError

CREATE PROCEDURE IAItemErrorDeleteAuto

@ItemErrorID INT

AS 

DELETE FROM [dbo].[IAItemError]

WHERE

	[ItemErrorID] = @ItemErrorID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAItemErrorDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

