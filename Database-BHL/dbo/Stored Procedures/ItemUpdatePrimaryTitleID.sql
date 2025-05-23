SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ItemUpdatePrimaryTitleID]

@ItemID INT,
@TitleID INT

AS 

SET NOCOUNT ON

UPDATE	it
SET		IsPrimary = CASE WHEN TitleID = @TitleID THEN 1 ELSE 0 END,
		it.LastModifiedDate = GETDATE()
FROM	dbo.ItemTitle it
WHERE	it.ItemID = @ItemID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemUpdatePrimaryTitleID. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT	BookID AS ItemID
	FROM	dbo.Book
	WHERE	BookID = @ItemID
	
	RETURN -- update successful
END

GO
