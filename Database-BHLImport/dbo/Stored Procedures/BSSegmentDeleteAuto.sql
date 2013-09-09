
-- BSSegmentDeleteAuto PROCEDURE
-- Generated 10/24/2012 4:21:54 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for BSSegment

CREATE PROCEDURE BSSegmentDeleteAuto

@SegmentID INT

AS 

DELETE FROM [dbo].[BSSegment]

WHERE

	[SegmentID] = @SegmentID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure BSSegmentDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

