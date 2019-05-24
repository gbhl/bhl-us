CREATE PROCEDURE [dbo].[ApiPageSelectBySegmentID]

@SegmentID INT

AS 

SET NOCOUNT ON

SELECT	sp.PageID,
		sp.SequenceOrder,
		p.[Year],
		p.Series,
		p.Volume,
		p.Issue,
		COALESCE(l.TextSource, 'OCR') AS TextSource,
		dbo.fnAPIIndicatedPageStringForPage(sp.PageID) AS PageNumbers,
		dbo.fnPageTypeStringForPage(sp.PageID) AS PageTypeName
FROM	dbo.SegmentPage sp 
		INNER JOIN dbo.Page p ON sp.PageID = p.PageID
		OUTER APPLY (
				SELECT  TOP 1 TextSource
				FROM    dbo.PageTextLog 
				WHERE   PageID = p.PageID
				ORDER BY PageTextLogID DESC
			) l
WHERE	sp.SegmentID = @SegmentID
ORDER BY
		sp.SequenceOrder ASC
