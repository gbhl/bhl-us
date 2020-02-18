CREATE PROCEDURE [dbo].[IASegmentPageSelectBySegmentAndSequence]

@SegmentID INT,
@PageSequence INT

AS 

SET NOCOUNT ON

SELECT	[SegmentPageID],
		[SegmentID],
		[PageSequence],
		[CreatedDate],
		[LastModifiedDate]
FROM	[dbo].[IASegmentPage]
WHERE	[SegmentID] = @SegmentID
AND		[PageSequence] = @PageSequence

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IASegmentPageSelectBySegmentAndSequence. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
