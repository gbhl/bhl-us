
CREATE PROCEDURE [dbo].[PageSummarySelectByPageId]
@PageId	int
AS 

SET NOCOUNT ON

SELECT MARCBibID, TitleID, RedirectTitleID, FullTitle, PartNumber, PartName, RareBooks, 
	ItemStatusID, ItemID, RedirectItemID, PrimaryTitleID, BarCode, Sponsor,
	v.PageID, FileNamePrefix, PageDescription, 
	CONVERT(int, x.SequenceOrder) AS SequenceOrder, 
	Illustration, PDFSize, ShortTitle, Volume, WebVirtualDirectory,
	OCRFolderShare, ExternalURL, AltExternalURL, DownloadUrl, ImageServerUrlFormat,
	FileRootFolder
FROM PageSummaryView v INNER JOIN (
				-- Computing alternate sequence order which is ensured to be exactly sequential (no gaps)
				SELECT	PageID, 
						ROW_NUMBER() OVER (PARTITION BY ItemID ORDER BY SequenceOrder) AS SequenceOrder
				FROM	Page
				WHERE	ItemID IN (SELECT ItemID FROM Page WHERE PageID = @PageID)
				AND		Active = 1
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
