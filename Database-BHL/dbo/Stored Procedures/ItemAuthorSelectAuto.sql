SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------

CREATE PROCEDURE [dbo].[ItemAuthorSelectAuto]

@ItemAuthorID INT

AS 

SET NOCOUNT ON

SELECT	
	[ItemAuthorID],
	[ItemID],
	[AuthorID],
	[SequenceOrder],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
FROM	
	[dbo].[ItemAuthor]
WHERE	
	[ItemAuthorID] = @ItemAuthorID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.ItemAuthorSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


GO
