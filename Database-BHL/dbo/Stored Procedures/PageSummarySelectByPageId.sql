SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PageSummarySelectByPageId]

@PageId	int

AS 

SET NOCOUNT ON

SELECT MARCBibID, TitleID, RedirectTitleID, FullTitle, PartNumber, PartName, RareBooks, 
	ItemStatusID, BookID, ItemID, RedirectBookID, PrimaryTitleID, BarCode, Sponsor,
	v.PageID, FileNamePrefix, PageDescription, 
	CONVERT(int, x.SequenceOrder) AS SequenceOrder, 
	Illustration, CAST(NULL AS INT) AS PDFSize, ShortTitle, Volume, WebVirtualDirectory,
	OCRFolderShare, ExternalURL, CAST(NULL AS NVARCHAR(1500)) AS AltExternalURL, DownloadUrl, ImageServerUrlFormat,
	FileRootFolder
FROM PageSummaryView v INNER JOIN (
				-- Computing alternate sequence order which is ensured to be exactly sequential (no gaps)
				SELECT	p.PageID, 
						ROW_NUMBER() OVER (PARTITION BY ip.ItemID ORDER BY ip.SequenceOrder) AS SequenceOrder
				FROM	dbo.Page p
						INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
				WHERE	ip.ItemID IN (SELECT ip.ItemID FROM dbo.Page p INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID WHERE p.PageID = @PageID)
				AND		p.Active = 1
				) x
		ON v.PageID = x.PageID
WHERE v.PageId = @PageId

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageSummarySelectByPageId. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


GO
