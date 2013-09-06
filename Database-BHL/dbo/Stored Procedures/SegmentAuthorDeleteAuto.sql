
-- SegmentAuthorDeleteAuto PROCEDURE
-- Generated 9/18/2012 12:12:30 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for SegmentAuthor

CREATE PROCEDURE SegmentAuthorDeleteAuto

@SegmentAuthorID INT

AS 

DELETE FROM [dbo].[SegmentAuthor]

WHERE

	[SegmentAuthorID] = @SegmentAuthorID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentAuthorDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

