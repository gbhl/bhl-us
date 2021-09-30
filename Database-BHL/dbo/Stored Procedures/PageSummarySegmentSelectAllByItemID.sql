CREATE PROCEDURE dbo.PageSummarySegmentSelectAllByItemID

@ItemID int

AS 

SET NOCOUNT ON

	SELECT	MARCBibID, TitleID, RedirectTitleID, FullTitle, PartNumber, PartName, RareBooks, 
			BookID, ItemID, ItemStatusID, RedirectBookID, PrimaryTitleID, BarCode, Sponsor, IsVirtual,
			PageID, FileNamePrefix, PageDescription, SequenceOrder, 
			Illustration, CAST(NULL AS INT) AS PDFSize, ShortTitle, Volume, WebVirtualDirectory,
			OCRFolderShare, ExternalURL, CAST(NULL AS NVARCHAR(1500)) AS AltExternalURL, DownloadUrl, ImageServerUrlFormat,
			FileRootFolder
	FROM	dbo.PageSummarySegmentView
	WHERE	ItemID = @ItemID 
	AND		Active=1
	ORDER BY SequenceOrder

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageSummarySelectAllByItemID. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
