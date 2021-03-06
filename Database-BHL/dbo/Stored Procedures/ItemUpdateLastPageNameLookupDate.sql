SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ItemUpdateLastPageNameLookupDate]

@ItemID INT

AS 

SET NOCOUNT ON

UPDATE	dbo.Book
SET		LastPageNameLookupDate = GETDATE(),
		LastModifiedDate = GETDATE()
WHERE	BookID = @ItemID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemUpdateLastPageNameLookupDate. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT	BookID AS ItemID
	FROM	dbo.Book
	WHERE	BookID = @ItemID
	
	RETURN -- update successful
END



GO
