SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------

CREATE PROCEDURE [dbo].[ItemKeywordUpdateAuto]

@ItemKeywordID INT,
@ItemID INT,
@KeywordID INT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[ItemKeyword]
SET
	[ItemID] = @ItemID,
	[KeywordID] = @KeywordID,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID
WHERE
	[ItemKeywordID] = @ItemKeywordID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.ItemKeywordUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

GO
