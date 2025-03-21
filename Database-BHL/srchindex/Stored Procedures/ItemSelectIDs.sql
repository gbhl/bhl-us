SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [srchindex].[ItemSelectIDs]

@StartID int

AS 

BEGIN

SET NOCOUNT ON

SELECT DISTINCT 
		b.BookID AS ItemID
FROM	dbo.Item i
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
		INNER JOIN dbo.ItemTitle it ON i.ItemID = it.ItemiD
		INNER JOIN dbo.Title t ON it.TitleID = t.TitleID
WHERE	i.ItemStatusID = 40
AND		t.PublishReady = 1
AND		b.BookID >= @StartID
ORDER BY b.BookID

END

GO
