
CREATE PROCEDURE [dbo].[PageMetadataSelectByPageID]

@PageID INT

AS 

SET NOCOUNT ON

SELECT	p.PageID,
		p.SequenceOrder,
		ISNULL(p.Year, i.Year) AS [Year],
		p.Series,
		ISNULL(CONVERT(nvarchar(100), p.Volume), i.Volume) AS Volume,
		p.IssuePrefix,
		p.Issue,
		dbo.fnIndicatedPageStringForPage(p.PageID) AS IndicatedPages,
		dbo.fnPageTypeStringForPage(p.PageID) AS PageTypes,
		p.FileNamePrefix,
		i.BarCode,
		t.MARCBibID,
		t.ShortTitle
FROM	dbo.Page p INNER JOIN dbo.Item i 
			ON p.ItemID = i.ItemID
		INNER JOIN dbo.Title t 
			ON i.PrimaryTitleID = t.TitleID
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
