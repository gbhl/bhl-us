SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PageSelectCountByItemID]

@ItemID INT

AS 

SET NOCOUNT ON

SELECT COUNT(*)
FROM [dbo].[Page] p
	INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
	INNER JOIN dbo.Book b ON ip.ItemID = b.ItemID
WHERE
	BookID = @ItemID
AND	[Active] = 1

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageSelectCountByItemID. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


GO
