SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------

CREATE PROCEDURE [dbo].[ItemKeywordInsertAuto]

@ItemKeywordID INT OUTPUT,
@ItemID INT,
@KeywordID INT,
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[ItemKeyword]
( 	[ItemID],
	[KeywordID],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID] )
VALUES
( 	@ItemID,
	@KeywordID,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID )

SET @ItemKeywordID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.ItemKeywordInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[ItemKeywordID],
		[ItemID],
		[KeywordID],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]	
	FROM [dbo].[ItemKeyword]
	WHERE
		[ItemKeywordID] = @ItemKeywordID
	
	RETURN -- insert successful
END

GO
