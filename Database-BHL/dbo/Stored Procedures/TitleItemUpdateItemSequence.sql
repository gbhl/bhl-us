CREATE PROCEDURE [dbo].[TitleItemUpdateItemSequence]

@TitleID INT,
@ItemID INT,
@ItemSequence SMALLINT

AS 

SET NOCOUNT ON

UPDATE	[dbo].[TitleItem]
SET		[ItemSequence] = @ItemSequence,
		[LastModifiedDate] = GETDATE()
WHERE	[TitleID] = @TitleID
AND		[ItemID] = @ItemID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleItemUpdateItemSequence. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT 

		[TitleItemID],
		[TitleID],
		[ItemID],
		[ItemSequence],
		[CreationDate],
		[LastModifiedDate]

	FROM [dbo].[TitleItem]

	WHERE
		[TitleID] = @TitleID
	AND [ItemID] = @ItemID
	
	RETURN -- update successful
END

