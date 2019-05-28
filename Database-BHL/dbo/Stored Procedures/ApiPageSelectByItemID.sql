CREATE PROCEDURE [dbo].[ApiPageSelectByItemID]

@ItemID INT

AS 

SET NOCOUNT ON

DECLARE @RedirItemID int
SELECT @RedirItemID = RedirectItemID FROM dbo.Item WHERE ItemID = @ItemID

IF (@RedirItemID IS NOT NULL)
	exec [dbo].[ApiPageSelectByItemID] @RedirItemID
ELSE
	SELECT	p.PageID,
			p.ItemID,
			p.SequenceOrder,
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
	WHERE	p.ItemID = @ItemID
	AND		p.Active = 1
	ORDER BY
			p.SequenceOrder ASC
