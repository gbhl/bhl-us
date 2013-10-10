CREATE PROCEDURE [dbo].[NameMetadataSelectByItemID]

@ItemID INT

AS 

SET NOCOUNT ON

-- Get Page Info
SELECT	p.PageID,
		p.SequenceOrder,
		dbo.fnIndicatedPageStringForPage(p.PageID) AS IndicatedPages,
		dbo.fnPageTypeStringForPage(p.PageID) AS PageTypes
INTO	#tmpPage
FROM	dbo.Page p WITH (NOLOCK)
WHERE	p.ItemID = @ItemID
AND		p.Active = 1

-- Get Name info
SELECT	p.PageID,
		p.SequenceOrder,
		nr.NameResolvedID,
		nr.ResolvedNameString
INTO	#tmpName
FROM	#tmpPage p WITH (NOLOCK)
		INNER JOIN NamePage np WITH (NOLOCK) ON p.PageID = np.PageID
		INNER JOIN Name n WITH (NOLOCK) ON np.NameID = n.NameID
		INNER JOIN NameResolved nr WITH (NOLOCK) ON n.NameResolvedID = nr.NameResolvedID

-- Combine page and name information
SELECT DISTINCT
		p.PageID,
		p.SequenceOrder,
		p.IndicatedPages,
		p.PageTypes,
		n.NameResolvedID,
		n.ResolvedNameString
FROM	#tmpPage p
		LEFT JOIN #tmpName n on p.PageID = n.PageID
ORDER BY
		p.SequenceOrder, p.PageID, n.ResolvedNameString ASC
