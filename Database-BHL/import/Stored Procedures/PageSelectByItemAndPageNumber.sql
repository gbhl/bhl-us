SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [import].[PageSelectByItemAndPageNumber]

@ItemID int,
@Volume nvarchar(20),
@Issue nvarchar(10),
@PageNumber nvarchar(20)

AS

BEGIN

SET NOCOUNT ON

SELECT	p.PageID
FROM	dbo.IndicatedPage ipg
		INNER JOIN dbo.Page p ON ipg.PageID = p.PageID
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		INNER JOIN dbo.Book b ON ip.ItemID = b.ItemID
WHERE	b.BookID = @ItemID
AND		p.Active = 1
AND		ipg.PageNumber = @PageNumber
AND		(
		ipg.PagePrefix IN ('', 'Page', 'p.') OR
		ipg.PagePrefix LIKE '% Page'
		)
AND		(
		ISNULL(p.Volume, 'PGVOLNULL') = ISNULL(@Volume, 'VARNULL') OR
		(ISNULL(b.StartVolume, 'ITMVOLNULL') = ISNULL(@Volume, 'VARNULL') AND ISNULL(p.Volume, '') = '') OR
		(ISNULL(b.StartVolume, '') = '' AND ISNULL(p.Volume, '') = '') OR
		ISNULL(@Volume, '') = ''
		)
AND		(
		ISNULL(p.Issue, 'PGISSNULL') = ISNULL(@Issue, 'VARNULL') OR
		(ISNULL(b.StartIssue, 'ITMVOLNULL') = ISNULL(@Issue, 'VARNULL') AND ISNULL(p.Issue, '') = '') OR
		(ISNULL(b.StartIssue, '') = '' AND ISNULL(p.Issue, '') = '') OR
		ISNULL(@Issue, '') = ''
		)

END


GO
