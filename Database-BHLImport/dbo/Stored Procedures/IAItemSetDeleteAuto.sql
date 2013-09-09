
-- IAItemSetDeleteAuto PROCEDURE
-- Generated 12/28/2007 12:46:28 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for IAItemSet

CREATE PROCEDURE IAItemSetDeleteAuto

@ItemID INT,
@SetID INT

AS 

DELETE FROM [dbo].[IAItemSet]

WHERE

	[ItemID] = @ItemID AND
	[SetID] = @SetID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAItemSetDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

