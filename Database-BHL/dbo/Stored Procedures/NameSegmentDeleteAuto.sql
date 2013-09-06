
-- NameSegmentDeleteAuto PROCEDURE
-- Generated 11/2/2012 3:47:04 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for NameSegment

CREATE PROCEDURE NameSegmentDeleteAuto

@NameSegmentID INT

AS 

DELETE FROM [dbo].[NameSegment]

WHERE

	[NameSegmentID] = @NameSegmentID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure NameSegmentDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

