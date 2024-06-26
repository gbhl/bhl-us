SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------

CREATE PROCEDURE [dbo].[ItemIdentifierInsertAuto]

@ItemIdentifierID INT OUTPUT,
@ItemID INT,
@IdentifierID INT,
@IdentifierValue NVARCHAR(125),
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[ItemIdentifier]
( 	[ItemID],
	[IdentifierID],
	[IdentifierValue],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID] )
VALUES
( 	@ItemID,
	@IdentifierID,
	@IdentifierValue,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID )

SET @ItemIdentifierID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.ItemIdentifierInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[ItemIdentifierID],
		[ItemID],
		[IdentifierID],
		[IdentifierValue],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]	
	FROM [dbo].[ItemIdentifier]
	WHERE
		[ItemIdentifierID] = @ItemIdentifierID
	
	RETURN -- insert successful
END

GO
