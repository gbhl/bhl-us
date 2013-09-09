
-- IASetDeleteAuto PROCEDURE
-- Generated 5/27/2008 11:38:08 AM
-- Do not modify the contents of this procedure.
-- Delete Procedure for IASet

CREATE PROCEDURE IASetDeleteAuto

@SetID INT

AS 

DELETE FROM [dbo].[IASet]

WHERE

	[SetID] = @SetID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IASetDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

