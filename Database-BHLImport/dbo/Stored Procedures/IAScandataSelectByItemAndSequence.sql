CREATE PROCEDURE [dbo].[IAScandataSelectByItemAndSequence]

@ItemID INT,
@Sequence INT

AS 

SET NOCOUNT ON

SELECT 

	[ScandataID],
	[ItemID],
	[Sequence],
	[PageType],
	[PageNumber],
	[CreatedDate],
	[LastModifiedDate]

FROM [dbo].[IAScandata]

WHERE
	[ItemID] = @ItemID
AND	[Sequence] = @Sequence

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAScandataSelectByItemAndSequence. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


