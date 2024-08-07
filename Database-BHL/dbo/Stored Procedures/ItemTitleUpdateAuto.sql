SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------

CREATE PROCEDURE [dbo].[ItemTitleUpdateAuto]

@ItemTitleID INT,
@ItemID INT,
@TitleID INT,
@ItemSequence SMALLINT,
@IsPrimary SMALLINT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[ItemTitle]
SET
	[ItemID] = @ItemID,
	[TitleID] = @TitleID,
	[ItemSequence] = @ItemSequence,
	[IsPrimary] = @IsPrimary,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID
WHERE
	[ItemTitleID] = @ItemTitleID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.ItemTitleUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[ItemTitleID],
		[ItemID],
		[TitleID],
		[ItemSequence],
		[IsPrimary],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]
	FROM [dbo].[ItemTitle]
	WHERE
		[ItemTitleID] = @ItemTitleID
	
	RETURN -- update successful
END

GO
