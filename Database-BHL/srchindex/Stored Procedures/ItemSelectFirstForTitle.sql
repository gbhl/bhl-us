SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [srchindex].[ItemSelectFirstForTitle]

@TitleID int

AS 

BEGIN

SET NOCOUNT ON

SELECT	b.BookID AS ItemID
FROM	dbo.ItemTitle itl
		INNER JOIN dbo.Book b ON itl.ItemID = b.ItemID
WHERE	TitleID = @TitleID
AND		ItemSequence IN (
			SELECT	MIN(ItemSequence) 
			FROM	dbo.ItemTitle it INNER JOIN dbo.Item i ON it.ItemID = i.ItemID
			WHERE	it.TitleID = @TitleID
			AND		i.ItemStatusID = 40
			)

END


GO
