SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [srchindex].[ItemCountForTitle]

@TitleID int

AS 

BEGIN

SET NOCOUNT ON

-- Return the current number of published items for the specified title
SELECT	b.BookID AS ItemID
FROM	dbo.ItemTitle it
		INNER JOIN dbo.Item i ON it.ItemID = i.ItemID
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
		INNER JOIN dbo.Title t ON it.TitleID = t.TitleID
WHERE	it.TitleID = @TitleID
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1

END


GO
