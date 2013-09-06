
CREATE PROCEDURE [dbo].[PageSelectFirstPageForItem]

@ItemID int

AS
BEGIN

CREATE TABLE #tmpPages (SequenceOrder int NULL, PageID int NOT NULL, PageTypeID int NULL)

INSERT INTO #tmpPages
SELECT	ROW_NUMBER() OVER (ORDER BY MIN(p.SequenceOrder)) AS SequenceOrder, 
		p.PageID, 
		MIN(ppt.PageTypeID)
FROM	Page p LEFT JOIN Page_PageType ppt
			ON p.PageID = ppt.PageID
WHERE	p.Active = 1
AND		p.ItemID = @ItemID
GROUP BY p.PageID

IF EXISTS(SELECT PageID FROM #tmpPages WHERE PageTypeID = 1)
BEGIN
	SELECT	PageID, SequenceOrder
	FROM	#tmpPages
	WHERE	PageTypeID = 1
	ORDER BY SequenceOrder
END
ELSE
BEGIN
	SELECT	PageID, SequenceOrder
	FROM	#tmpPages
	WHERE	SequenceOrder = 1
END

END



