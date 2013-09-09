
-- IAItemSetUpdateAuto PROCEDURE
-- Generated 12/28/2007 12:46:28 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for IAItemSet

CREATE PROCEDURE IAItemSetUpdateAuto

@ItemID INT,
@SetID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[IAItemSet]

SET

	[ItemID] = @ItemID,
	[SetID] = @SetID

WHERE
	[ItemID] = @ItemID AND
	[SetID] = @SetID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAItemSetUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[ItemID],
		[SetID],
		[CreatedDate]

	FROM [dbo].[IAItemSet]
	
	WHERE
		[ItemID] = @ItemID AND 
		[SetID] = @SetID
	
	RETURN -- update successful
END

