
-- BSItemUpdateAuto PROCEDURE
-- Generated 10/23/2012 3:54:50 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for BSItem

CREATE PROCEDURE BSItemUpdateAuto

@ItemID INT,
@BHLItemID INT,
@ItemStatusID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[BSItem]

SET

	[BHLItemID] = @BHLItemID,
	[ItemStatusID] = @ItemStatusID,
	[LastModifiedDate] = getdate()

WHERE
	[ItemID] = @ItemID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure BSItemUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[ItemID],
		[BHLItemID],
		[ItemStatusID],
		[CreationDate],
		[LastModifiedDate]

	FROM [dbo].[BSItem]
	
	WHERE
		[ItemID] = @ItemID
	
	RETURN -- update successful
END

