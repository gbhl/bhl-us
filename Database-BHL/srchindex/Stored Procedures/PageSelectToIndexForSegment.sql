CREATE PROCEDURE [srchindex].[PageSelectToIndexForSegment]

@SegmentID int

AS 

BEGIN

SET NOCOUNT ON

SELECT	p.PageID,
		s.ItemID,
		CASE WHEN b.IsVirtual = 0 THEN bp.SequenceOrder ELSE sp.SequenceOrder END AS SequenceOrder,
		COALESCE(sv.OcrFolderSharE, Bv.OcrFolderShare) AS OcrFolderShare,
		CASE WHEN b.IsVirtual = 0 THEN bi.FileRootFolder ELSE si.FileRootFolder END AS FileRootFolder,
		CASE WHEN b.IsVirtual = 0 THEN b.BarCode ELSE s.BarCode END AS BarCode,
		p.FileNamePrefix,
		dbo.fnIndicatedPageStringForPage(p.PageID) AS PageIndicators,
		dbo.fnPageTypeStringForPage(p.PageID) AS PageTypes
FROM	dbo.ItemPage sp WITH (NOLOCK)
		INNER JOIN dbo.Page p  WITH (NOLOCK) ON sp.PageID = p.PageID
		INNER JOIN dbo.Segment s WITH (NOLOCK) ON sp.ItemID = s.ItemID
		INNER JOIN dbo.Item si WITH (NOLOCK) ON s.ItemID = si.Itemid
		INNER JOIN dbo.ItemRelationship ir WITH (NOLOCK) ON si.ItemID = ir.ChildID
		INNER JOIN dbo.Item bi WITH (NOLOCK) ON ir.ParentID = bi.ItemID
		LEFT JOIN dbo.ItemPage bp WITH (NOLOCK) ON bi.ItemID = bp.ItemID AND (sp.PageID = bp.PageID or bp.PageID IS NULL)
		INNER JOIN dbo.Book b WITH (NOLOCK) ON bi.ItemID = b.ItemID
		LEFT JOIN dbo.Vault bv WITH (NOLOCK) ON bi.VaultID = bv.VaultID
		LEFT JOIN dbo.Vault sv WITH (NOLOCK) ON si.VaultID = sv.VaultID
WHERE	s.SegmentID = @SegmentID
AND		p.Active = 1
ORDER BY sp.SequenceOrder

END

GO
