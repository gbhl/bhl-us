SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------

CREATE PROCEDURE [dbo].[ItemIdentifierUpdateAuto]

@ItemIdentifierID INT,
@ItemID INT,
@IdentifierID INT,
@IdentifierValue NVARCHAR(125),
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[ItemIdentifier]
SET
	[ItemID] = @ItemID,
	[IdentifierID] = @IdentifierID,
	[IdentifierValue] = @IdentifierValue,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID
WHERE
	[ItemIdentifierID] = @ItemIdentifierID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.ItemIdentifierUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

GO
