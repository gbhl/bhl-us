SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ItemSelectAuto]

@ItemID INT

AS 

SET NOCOUNT ON

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
FROM	
	[dbo].[Item]
WHERE	
	[ItemID] = @ItemID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.ItemSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


GO
