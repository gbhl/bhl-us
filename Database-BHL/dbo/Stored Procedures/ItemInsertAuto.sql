SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ItemInsertAuto]

@ItemID INT OUTPUT,
@ItemTypeID INT = null,
@ItemStatusID INT,
@ItemSourceID INT = null,
@VaultID INT = null,
@FileRootFolder NVARCHAR(250) = null,
@ItemDescription NVARCHAR(MAX) = null,
@Note NVARCHAR(MAX) = null,
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Item]
( 	[ItemTypeID],
	[ItemStatusID],
	[ItemSourceID],
	[VaultID],
	[FileRootFolder],
	[ItemDescription],
	[Note],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID] )
VALUES
( 	@ItemTypeID,
	@ItemStatusID,
	@ItemSourceID,
	@VaultID,
	@FileRootFolder,
	@ItemDescription,
	@Note,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID )

SET @ItemID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.ItemInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[ItemID],
		[ItemTypeID],
		[ItemStatusID],
		[ItemSourceID],
		[VaultID],
		[FileRootFolder],
		[ItemDescription],
		[Note],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]	
	FROM [dbo].[Item]
	WHERE
		[ItemID] = @ItemID
	
	RETURN -- insert successful
END


GO
