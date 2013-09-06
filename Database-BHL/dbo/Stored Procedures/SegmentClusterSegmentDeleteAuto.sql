
-- SegmentClusterSegmentDeleteAuto PROCEDURE
-- Generated 9/18/2012 12:12:30 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for SegmentClusterSegment

CREATE PROCEDURE SegmentClusterSegmentDeleteAuto

@SegmentID INT

AS 

DELETE FROM [dbo].[SegmentClusterSegment]

WHERE

	[SegmentID] = @SegmentID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentClusterSegmentDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

