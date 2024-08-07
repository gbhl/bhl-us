SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------

CREATE PROCEDURE [dbo].[ItemKeywordSelectAuto]

@ItemKeywordID INT

AS 

SET NOCOUNT ON

SELECT	
	[ItemKeywordID],
	[ItemID],
	[KeywordID],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
FROM	
	[dbo].[ItemKeyword]
WHERE	
	[ItemKeywordID] = @ItemKeywordID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.ItemKeywordSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


GO
