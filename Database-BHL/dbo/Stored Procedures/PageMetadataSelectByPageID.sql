CREATE PROCEDURE [dbo].[PageMetadataSelectByPageID]

@PageID INT

AS 

SET NOCOUNT ON

SELECT	p.PageID,
		b.BookID AS ItemID,
		ip.SequenceOrder,
		ISNULL(p.Year, b.StartYear) AS [Year],
		p.Series,
		ISNULL(CONVERT(nvarchar(100), p.Volume), b.StartVolume) AS Volume,
		p.IssuePrefix,
		p.Issue,
		dbo.fnIndicatedPageStringForPage(p.PageID) AS IndicatedPages,
		dbo.fnPageTypeStringForPage(p.PageID) AS PageTypes,
		p.FileNamePrefix,
		b.BarCode,
		t.MARCBibID,
		t.ShortTitle
FROM	dbo.Page p 
		INNER JOIN dbo.ItemPage ip on p.PageID = ip.PageID
		INNER JOIN dbo.Item i ON ip.ItemID = i.ItemID
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
		INNER JOIN dbo.ItemTitle it ON i.ItemID = it.ItemID AND it.IsPrimary = 1
		INNER JOIN dbo.Title t ON it.TitleID = t.TitleID
WHERE	p.[PageID] = @PageID
UNION
SELECT	p.PageID,
		s.SegmentID AS ItemID,
		ip.SequenceOrder,
		ISNULL(p.Year, s.Date) AS [Year],
		p.Series,
		ISNULL(CONVERT(nvarchar(100), p.Volume), s.Volume) AS Volume,
		p.IssuePrefix,
		p.Issue,
		dbo.fnIndicatedPageStringForPage(p.PageID) AS IndicatedPages,
		dbo.fnPageTypeStringForPage(p.PageID) AS PageTypes,
		p.FileNamePrefix,
		s.BarCode,
		t.MARCBibID,
		t.ShortTitle
FROM	dbo.Page p 
		INNER JOIN dbo.ItemPage ip on p.PageID = ip.PageID
		INNER JOIN dbo.vwSegment s ON ip.ItemID = s.ItemID
		INNER JOIN dbo.Book b ON s.BookID = b.BookID AND b.IsVirtual = 1
		INNER JOIN dbo.ItemTitle it ON b.ItemID = it.ItemID AND it.IsPrimary = 1
		INNER JOIN dbo.Title t ON it.TitleID = t.TitleID
WHERE	p.[PageID] = @PageID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageMetadataSelectByPageID. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
