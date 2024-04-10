CREATE PROCEDURE dbo.IABHLCreatorInsertAuto

@BHLCreatorID INT OUTPUT,
@ItemID INT,
@Name NVARCHAR(300)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[IABHLCreator]
( 	[ItemID],
	[Name],
	[CreatedDate],
	[LastModifiedDate] )
VALUES
( 	@ItemID,
	@Name,
	getdate(),
	getdate() )

SET @BHLCreatorID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.IABHLCreatorInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END
GO
