SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------

CREATE PROCEDURE [dbo].[ItemAuthorInsertAuto]

@ItemAuthorID INT OUTPUT,
@ItemID INT,
@AuthorID INT,
@SequenceOrder SMALLINT,
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[ItemAuthor]
( 	[ItemID],
	[AuthorID],
	[SequenceOrder],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID] )
VALUES
( 	@ItemID,
	@AuthorID,
	@SequenceOrder,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID )

SET @ItemAuthorID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.ItemAuthorInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[ItemAuthorID],
		[ItemID],
		[AuthorID],
		[SequenceOrder],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]	
	FROM [dbo].[ItemAuthor]
	WHERE
		[ItemAuthorID] = @ItemAuthorID
	
	RETURN -- insert successful
END

GO
