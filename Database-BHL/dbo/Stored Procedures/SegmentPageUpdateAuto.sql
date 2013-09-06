
-- SegmentPageUpdateAuto PROCEDURE
-- Generated 9/18/2012 12:12:30 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for SegmentPage

CREATE PROCEDURE SegmentPageUpdateAuto

@SegmentPageID INT,
@SegmentID INT,
@PageID INT,
@SequenceOrder SMALLINT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[SegmentPage]

SET

	[SegmentID] = @SegmentID,
	[PageID] = @PageID,
	[SequenceOrder] = @SequenceOrder,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[SegmentPageID] = @SegmentPageID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentPageUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[SegmentPageID],
		[SegmentID],
		[PageID],
		[SequenceOrder],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]

	FROM [dbo].[SegmentPage]
	
	WHERE
		[SegmentPageID] = @SegmentPageID
	
	RETURN -- update successful
END

