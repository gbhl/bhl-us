
CREATE PROCEDURE [dbo].[PageSummarySelectByBibID]
@BibID	nvarchar(50)
AS 

SET NOCOUNT ON

	SELECT TOP 1 MARCBibID, TitleID, FullTitle,  PartNumber, PartName, RareBooks, 
		ItemStatusID, ItemID, BarCode, PageID, FileNamePrefix, PageDescription, SequenceOrder, 
		Illustration, PDFSize, ShortTitle, Volume, WebVirtualDirectory,
		OCRFolderShare, ExternalURL, AltExternalURL, DownloadUrl, FileRootFolder
	FROM PageSummaryView
	WHERE MARCBibID = @BibID AND SequenceOrder=1 AND Active=1 AND ItemStatusID=40
	ORDER BY ItemSequence

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageSummarySelectByBibID. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


