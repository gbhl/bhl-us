CREATE PROCEDURE [dbo].[PageSummarySelectAllByItemID]

@ItemID int

AS 

SET NOCOUNT ON

	SELECT	MARCBibID, TitleID, RedirectTitleID, FullTitle, PartNumber, PartName, RareBooks, 
			ItemStatusID, ItemID, RedirectItemID, PrimaryTitleID, BarCode, Sponsor,
			PageID, FileNamePrefix, PageDescription, SequenceOrder, 
			Illustration, PDFSize, ShortTitle, Volume, WebVirtualDirectory,
			OCRFolderShare, ExternalURL, AltExternalURL, DownloadUrl, ImageServerUrlFormat,
			FileRootFolder
	FROM	dbo.PageSummaryView
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
