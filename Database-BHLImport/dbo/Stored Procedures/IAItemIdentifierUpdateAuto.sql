CREATE PROCEDURE dbo.IAItemIdentifierUpdateAuto

@ItemIdentifierID INT,
@ItemID INT,
@IdentifierDescription NVARCHAR(100),
@IdentifierValue NVARCHAR(125)

AS 

SET NOCOUNT ON

UPDATE [dbo].[IAItemIdentifier]
SET
	[ItemID] = @ItemID,
	[IdentifierDescription] = @IdentifierDescription,
	[IdentifierValue] = @IdentifierValue,
	[LastModifiedDate] = getdate()
WHERE
	[ItemIdentifierID] = @ItemIdentifierID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.IAItemIdentifierUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[ItemIdentifierID],
		[ItemID],
		[IdentifierDescription],
		[IdentifierValue],
		[CreatedDate],
		[LastModifiedDate]
	FROM [dbo].[IAItemIdentifier]
	WHERE
		[ItemIdentifierID] = @ItemIdentifierID
	
	RETURN -- update successful
END
