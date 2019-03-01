CREATE PROCEDURE dbo.IAItemIdentifierInsertAuto

@ItemIdentifierID INT OUTPUT,
@ItemID INT,
@IdentifierDescription NVARCHAR(100),
@IdentifierValue NVARCHAR(125)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[IAItemIdentifier]
( 	[ItemID],
	[IdentifierDescription],
	[IdentifierValue],
	[CreatedDate],
	[LastModifiedDate] )
VALUES
( 	@ItemID,
	@IdentifierDescription,
	@IdentifierValue,
	getdate(),
	getdate() )

SET @ItemIdentifierID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.IAItemIdentifierInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END
