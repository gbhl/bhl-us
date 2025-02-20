SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ApiPageSelectByItemID]

@ItemID INT

AS 

SET NOCOUNT ON

DECLARE @RedirItemID int
SELECT @RedirItemID = RedirectBookID FROM dbo.Book WHERE BookID = @ItemID

IF (@RedirItemID IS NOT NULL)
	exec [dbo].[ApiPageSelectByItemID] @RedirItemID
ELSE
	SELECT	p.PageID,
			b.BookID AS ItemID,
			ip.SequenceOrder,
			p.[Year],
			p.Series,
			p.Volume,
			p.Issue,
			COALESCE(l.TextSource, 'OCR') AS TextSource,
			dbo.fnAPIIndicatedPageStringForPage(p.PageID) AS PageNumbers,
			dbo.fnPageTypeStringForPage(p.PageID) AS PageTypeName
	FROM	dbo.Page p 
			OUTER APPLY (
					SELECT  TOP 1 TextSource
					FROM    dbo.PageTextLog 
					WHERE   PageID = p.PageID
					ORDER BY PageTextLogID DESC
				) l
			INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
			INNER JOIN dbo.Book b ON ip.ItemID = b.ItemID
	WHERE	b.BookID = @ItemID
	AND		p.Active = 1
	ORDER BY
			ip.SequenceOrder ASC


GO
