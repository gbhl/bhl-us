CREATE PROCEDURE [dbo].[NameMetadataSelectByItemID]

@ItemID INT

AS 

SET NOCOUNT ON

-- Get Page Info
SELECT	p.PageID,
		p.SequenceOrder,
		dbo.fnIndicatedPageStringForPage(p.PageID) AS IndicatedPages,
		dbo.fnPageTypeStringForPage(p.PageID) AS PageTypes,
		nr.NameResolvedID,
		nr.ResolvedNameString
FROM	dbo.Page p WITH (NOLOCK)
		LEFT JOIN NamePage np WITH (NOLOCK) ON p.PageID = np.PageID
		LEFT JOIN Name n WITH (NOLOCK) ON np.NameID = n.NameID
		LEFT JOIN NameResolved nr WITH (NOLOCK) ON n.NameResolvedID = nr.NameResolvedID
WHERE	p.ItemID = @ItemID
AND		p.Active = 1
ORDER BY
		p.SequenceOrder, p.PageID, nr.ResolvedNameString ASC

