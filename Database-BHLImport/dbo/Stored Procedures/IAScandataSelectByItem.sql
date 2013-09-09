CREATE PROCEDURE [dbo].[IAScandataSelectByItem]

@ItemID INT

AS 

SET NOCOUNT ON

SELECT	[ScandataID],
		[ItemID],
		[Sequence],
		[PageType],
		[PageNumber],
		[CreatedDate],
		[LastModifiedDate]
FROM	[dbo].[IAScandata]
WHERE	[ItemID] = @ItemID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAScandataSelectByItem. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

