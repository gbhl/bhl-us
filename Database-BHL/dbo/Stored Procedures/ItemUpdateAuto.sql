SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ItemUpdateAuto]

@ItemID INT,
@ItemTypeID INT,
@ItemStatusID INT,
@ItemSourceID INT,
@VaultID INT,
@FileRootFolder NVARCHAR(250),
@ItemDescription NVARCHAR(MAX),
@Note NVARCHAR(MAX),
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[Item]
SET
	[ItemTypeID] = @ItemTypeID,
	[ItemStatusID] = @ItemStatusID,
	[ItemSourceID] = @ItemSourceID,
	[VaultID] = @VaultID,
	[FileRootFolder] = @FileRootFolder,
	[ItemDescription] = @ItemDescription,
	[Note] = @Note,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID
WHERE
	[ItemID] = @ItemID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.ItemUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END


GO
