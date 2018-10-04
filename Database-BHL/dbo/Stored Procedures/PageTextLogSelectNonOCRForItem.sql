CREATE PROCEDURE dbo.PageTextLogSelectNonOCRForItem

@ItemID int

AS

BEGIN

SET NOCOUNT ON

-- Get the last log entry for each page
SELECT	p.PageID,
		MAX(PageTextLogID) AS PageTextLogID
INTO	#Log
FROM	dbo.Page p
		INNER JOIN dbo.PageTextLog l ON p.PageID = l.PageID
WHERE	p.ItemID = @ItemID
GROUP BY p.PageID

-- Return any of the most recent log entries that are NOT 'OCR'
SELECT	PageTextLogID
FROM	dbo.PageTextLog
WHERE	PageTextLogID IN (SELECT PageTextLogID FROM #Log)
AND		TextSource <> 'OCR'

END
