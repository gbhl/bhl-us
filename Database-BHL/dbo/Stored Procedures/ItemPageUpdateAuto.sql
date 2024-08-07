SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------

CREATE PROCEDURE [dbo].[ItemPageUpdateAuto]

@ItemPageID INT,
@ItemID INT,
@PageID INT,
@SequenceOrder INT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[ItemPage]
SET
	[ItemID] = @ItemID,
	[PageID] = @PageID,
	[SequenceOrder] = @SequenceOrder,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID
WHERE
	[ItemPageID] = @ItemPageID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.ItemPageUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[ItemPageID],
		[ItemID],
		[PageID],
		[SequenceOrder],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]
	FROM [dbo].[ItemPage]
	WHERE
		[ItemPageID] = @ItemPageID
	
	RETURN -- update successful
END

GO
