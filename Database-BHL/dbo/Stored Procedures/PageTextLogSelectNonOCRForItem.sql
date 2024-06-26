CREATE PROCEDURE [dbo].[PageTextLogSelectNonOCRForItem]

@ItemID int

AS

BEGIN

SET NOCOUNT ON

-- Get the last log entry for each page
SELECT	ip.PageID,
		MAX(PageTextLogID) AS PageTextLogID
INTO	#Log
FROM	dbo.ItemPage ip
		INNER JOIN dbo.PageTextLog l ON ip.PageID = l.PageID
WHERE	ip.ItemID = @ItemID
GROUP BY ip.PageID

-- Return any of the most recent log entries that are NOT 'OCR'
SELECT	PageTextLogID
FROM	dbo.PageTextLog
WHERE	PageTextLogID IN (SELECT PageTextLogID FROM #Log)
AND		TextSource <> 'OCR'

END

GO
