
-- TitleItemUpdateAuto PROCEDURE
-- Generated 2/4/2011 3:25:21 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for TitleItem

CREATE PROCEDURE TitleItemUpdateAuto

@TitleItemID INT,
@TitleID INT,
@ItemID INT,
@ItemSequence SMALLINT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[TitleItem]

SET

	[TitleID] = @TitleID,
	[ItemID] = @ItemID,
	[ItemSequence] = @ItemSequence,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[TitleItemID] = @TitleItemID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleItemUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[TitleItemID],
		[TitleID],
		[ItemID],
		[ItemSequence],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]

	FROM [dbo].[TitleItem]
	
	WHERE
		[TitleItemID] = @TitleItemID
	
	RETURN -- update successful
END

