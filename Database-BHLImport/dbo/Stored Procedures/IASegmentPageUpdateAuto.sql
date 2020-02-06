CREATE PROCEDURE dbo.IASegmentPageUpdateAuto

@SegmentPageID INT,
@SegmentID INT,
@PageSequence INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[IASegmentPage]
SET
	[SegmentID] = @SegmentID,
	[PageSequence] = @PageSequence,
	[LastModifiedDate] = getdate()
WHERE
	[SegmentPageID] = @SegmentPageID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.IASegmentPageUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[SegmentPageID],
		[SegmentID],
		[PageSequence],
		[CreatedDate],
		[LastModifiedDate]
	FROM [dbo].[IASegmentPage]
	WHERE
		[SegmentPageID] = @SegmentPageID
	
	RETURN -- update successful
END
