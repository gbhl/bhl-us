CREATE PROCEDURE [dbo].[ItemUpdateLastPageNameLookupDate]

@ItemID INT

AS 

BEGIN

SET NOCOUNT ON

BEGIN TRY

	-- Only one of these statements will actually perform an update
	UPDATE	dbo.Book
	SET		LastPageNameLookupDate = GETDATE(),
			LastModifiedDate = GETDATE()
	WHERE	ItemID = @ItemID

	UPDATE	dbo.Segment
	SET		LastPageNameLookupDate = GETDATE(),
			LastModifiedDate = GETDATE()
	WHERE	ItemID = @ItemID
		
END TRY
BEGIN CATCH
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemUpdateLastPageNameLookupDate. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END CATCH

END

GO
