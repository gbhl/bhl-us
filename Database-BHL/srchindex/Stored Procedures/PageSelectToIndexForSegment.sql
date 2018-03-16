CREATE PROCEDURE [srchindex].[PageSelectToIndexForSegment]

@SegmentID int

AS 

BEGIN

SET NOCOUNT ON

SELECT	p.PageID,
		p.ItemID,
		p.SequenceOrder,
		v.OcrFolderShare,
		i.FileRootFolder,
		i.BarCode,
		p.FileNamePrefix,
		dbo.fnIndicatedPageStringForPage(p.PageID) AS PageIndicators,
		dbo.fnPageTypeStringForPage(p.PageID) AS PageTypes
FROM	dbo.SegmentPage sp WITH (NOLOCK)
		INNER JOIN dbo.Page p  WITH (NOLOCK) ON sp.PageID = p.PageID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON p.ItemID = i.ItemID
		INNER JOIN dbo.Vault v WITH (NOLOCK) ON i.VaultID = v.VaultID
WHERE	sp.SegmentID = @SegmentID
ORDER BY p.SequenceOrder

END
