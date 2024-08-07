SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ApiPageSelectBySegmentID]

@SegmentID INT

AS 

SET NOCOUNT ON

SELECT	p.PageID,
		ip.SequenceOrder,
		p.[Year],
		p.Series,
		p.Volume,
		p.Issue,
		COALESCE(l.TextSource, 'OCR') AS TextSource,
		dbo.fnAPIIndicatedPageStringForPage(p.PageID) AS PageNumbers,
		dbo.fnPageTypeStringForPage(p.PageID) AS PageTypeName
FROM	dbo.Segment s
		INNER JOIN dbo.ItemPage ip ON s.ItemID = ip.ItemID
		INNER JOIN dbo.Page p ON ip.PageID = p.PageID
		OUTER APPLY (
				SELECT  TOP 1 TextSource
				FROM    dbo.PageTextLog 
				WHERE   PageID = p.PageID
				ORDER BY PageTextLogID DESC
			) l
WHERE	s.SegmentID = @SegmentID
ORDER BY
		ip.SequenceOrder ASC


GO
