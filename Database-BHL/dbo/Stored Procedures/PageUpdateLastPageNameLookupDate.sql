SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PageUpdateLastPageNameLookupDate]

@PageID INT

AS 

SET NOCOUNT ON

UPDATE	[dbo].[Page]
SET		[LastPageNameLookupDate] = GETDATE(),
		[LastModifiedDate] = GETDATE(),
		[LastModifiedUserID] = 1	-- system update
WHERE	[PageID] = @PageID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageUpdateLastPageNameLookupDate. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT	p.[PageID],
			b.BookID AS [ItemID],
			ip.[SequenceOrder],
			[Active]
	FROM	[dbo].[Page] p
			INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
			INNER JOIN dbo.Book b ON ip.ItemID = b.ItemID
	WHERE	p.[PageID] = @PageID
	
	RETURN -- update successful
END


GO
