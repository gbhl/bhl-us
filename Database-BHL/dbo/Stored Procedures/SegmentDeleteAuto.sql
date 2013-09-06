
-- SegmentDeleteAuto PROCEDURE
-- Generated 4/12/2013 11:25:53 AM
-- Do not modify the contents of this procedure.
-- Delete Procedure for Segment

CREATE PROCEDURE SegmentDeleteAuto

@SegmentID INT

AS 

DELETE FROM [dbo].[Segment]

WHERE

	[SegmentID] = @SegmentID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

