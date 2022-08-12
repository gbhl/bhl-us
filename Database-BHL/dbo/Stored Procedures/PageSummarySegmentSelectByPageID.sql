CREATE PROCEDURE [dbo].[PageSummarySegmentSelectByPageID]

@PageID int

AS 

SET NOCOUNT ON

SELECT MARCBibID, TitleID, RedirectTitleID, FullTitle, PartNumber, PartName, RareBooks, 
	ItemStatusID, BookID, ItemID, RedirectBookID, PrimaryTitleID, BarCode, Sponsor, IsVirtual,
	PageID, FileNamePrefix, PageDescription, SequenceOrder, 
	Illustration, CAST(NULL AS INT) AS PDFSize, ShortTitle, Volume, WebVirtualDirectory,
	OCRFolderShare, ExternalURL, CAST(NULL AS NVARCHAR(1500)) AS AltExternalURL, DownloadUrl, ImageServerUrlFormat,
	FileRootFolder
FROM PageSummarySegmentView
WHERE PageID = @PageID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageSummarySegmentSelectByPageID. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
