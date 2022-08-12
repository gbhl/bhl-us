CREATE PROCEDURE [dbo].[PageSummarySegmentSelectByItemAndSequence]

@ItemID	int,
@Sequence int

AS 

SET NOCOUNT ON

SELECT DISTINCT 
		BarCode, OCRFolderShare, FileRootFolder
FROM	dbo.PageSummarySegmentView
WHERE	ItemID = @ItemID 
AND		SequenceOrder = @Sequence

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageSummarySegmentSelectByItemAndSequence. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
