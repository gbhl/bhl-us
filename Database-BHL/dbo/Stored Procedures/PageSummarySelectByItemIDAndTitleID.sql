
CREATE PROCEDURE [dbo].[PageSummarySelectByItemIDAndTitleID]
@ItemID int,
@TitleID int
AS 

SET NOCOUNT ON

	SELECT MARCBibID, TitleID, RedirectTitleID, FullTitle, PartNumber, PartName, RareBooks, 
		ItemStatusID, ItemID, RedirectItemID, PrimaryTitleID, BarCode, 
		PageID, FileNamePrefix, PageDescription, SequenceOrder, 
		Illustration, PDFSize, ShortTitle, Volume, WebVirtualDirectory,
		OCRFolderShare, ExternalURL, AltExternalURL, DownloadUrl, ImageServerUrlFormat,
		FileRootFolder
	FROM PageSummaryView
	WHERE ItemID = @ItemID AND TitleID = @TitleID AND SequenceOrder=1 AND Active=1
	ORDER BY SortTitle

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageSummarySelectByItemIDAndTitleID. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END



