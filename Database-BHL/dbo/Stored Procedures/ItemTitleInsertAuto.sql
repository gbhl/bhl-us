SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------

CREATE PROCEDURE [dbo].[ItemTitleInsertAuto]

@ItemTitleID INT OUTPUT,
@ItemID INT,
@TitleID INT,
@ItemSequence SMALLINT = null,
@IsPrimary SMALLINT,
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[ItemTitle]
( 	[ItemID],
	[TitleID],
	[ItemSequence],
	[IsPrimary],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID] )
VALUES
( 	@ItemID,
	@TitleID,
	@ItemSequence,
	@IsPrimary,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID )

SET @ItemTitleID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.ItemTitleInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

GO
