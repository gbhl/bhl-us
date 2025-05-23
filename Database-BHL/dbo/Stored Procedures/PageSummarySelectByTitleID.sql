SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PageSummarySelectByTitleID]

@TitleID int

AS 

SET NOCOUNT ON

	SELECT TOP 1 MARCBibID, TitleID, FullTitle, PartNumber, PartName, RareBooks, ItemStatusID, 
		BookID, ItemID, BarCode, Sponsor, IsVirtual, PageID, FileNamePrefix, PageDescription, SequenceOrder, 
		Illustration, CAST(NULL AS INT) AS PDFSize, ShortTitle, Volume, WebVirtualDirectory,
		OCRFolderShare, ExternalURL, CAST(NULL AS NVARCHAR(1500)) AS AltExternalURL, DownloadUrl, ImageServerUrlFormat,
		FileRootFolder
	FROM PageSummaryView
	WHERE TitleID= @TitleID AND SequenceOrder=1 AND Active=1 AND ItemStatusID=40
	ORDER BY ItemSequence

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure [PageSummarySelectByTitleID]. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
