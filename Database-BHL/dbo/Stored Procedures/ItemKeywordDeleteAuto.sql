SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------

CREATE PROCEDURE [dbo].[ItemKeywordDeleteAuto]

@ItemKeywordID INT

AS 

SET NOCOUNT ON

DELETE 
FROM	
	[dbo].[ItemKeyword]
WHERE	
	[ItemKeywordID] = @ItemKeywordID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.ItemKeywordDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END


GO
