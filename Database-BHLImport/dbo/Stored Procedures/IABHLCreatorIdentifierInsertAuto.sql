CREATE PROCEDURE dbo.IABHLCreatorIdentifierInsertAuto

@BHLCreatorIdentifierID INT OUTPUT,
@BHLCreatorID INT,
@Type NVARCHAR(40),
@Value NVARCHAR(125)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[IABHLCreatorIdentifier]
( 	[BHLCreatorID],
	[Type],
	[Value],
	[CreatedDate],
	[LastModifiedDate] )
VALUES
( 	@BHLCreatorID,
	@Type,
	@Value,
	getdate(),
	getdate() )

SET @BHLCreatorIdentifierID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.IABHLCreatorIdentifierInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END
GO
