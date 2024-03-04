CREATE PROCEDURE dbo.IABHLCreatorUpdateAuto

@BHLCreatorID INT,
@ItemID INT,
@Name NVARCHAR(300)

AS 

SET NOCOUNT ON

UPDATE [dbo].[IABHLCreator]
SET
	[ItemID] = @ItemID,
	[Name] = @Name,
	[LastModifiedDate] = getdate()
WHERE
	[BHLCreatorID] = @BHLCreatorID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.IABHLCreatorUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[BHLCreatorID],
		[ItemID],
		[Name],
		[CreatedDate],
		[LastModifiedDate]
	FROM [dbo].[IABHLCreator]
	WHERE
		[BHLCreatorID] = @BHLCreatorID
	
	RETURN -- update successful
END
GO
