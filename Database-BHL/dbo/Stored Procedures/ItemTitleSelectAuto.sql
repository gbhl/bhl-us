SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------

CREATE PROCEDURE [dbo].[ItemTitleSelectAuto]

@ItemTitleID INT

AS 

SET NOCOUNT ON

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
FROM	
	[dbo].[ItemTitle]
WHERE	
	[ItemTitleID] = @ItemTitleID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.ItemTitleSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


GO
