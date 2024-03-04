CREATE PROCEDURE dbo.IABHLCreatorIdentifierUpdateAuto

@BHLCreatorIdentifierID INT,
@BHLCreatorID INT,
@Type NVARCHAR(40),
@Value NVARCHAR(125)

AS 

SET NOCOUNT ON

UPDATE [dbo].[IABHLCreatorIdentifier]
SET
	[BHLCreatorID] = @BHLCreatorID,
	[Type] = @Type,
	[Value] = @Value,
	[LastModifiedDate] = getdate()
WHERE
	[BHLCreatorIdentifierID] = @BHLCreatorIdentifierID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.IABHLCreatorIdentifierUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[BHLCreatorIdentifierID],
		[BHLCreatorID],
		[Type],
		[Value],
		[CreatedDate],
		[LastModifiedDate]
	FROM [dbo].[IABHLCreatorIdentifier]
	WHERE
		[BHLCreatorIdentifierID] = @BHLCreatorIdentifierID
	
	RETURN -- update successful
END
GO
