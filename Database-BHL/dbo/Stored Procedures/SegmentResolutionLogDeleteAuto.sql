CREATE PROCEDURE SegmentResolutionLogDeleteAuto

@SegmentResolutionLogID INT

AS 

DELETE FROM [dbo].[SegmentResolutionLog]

WHERE

	[SegmentResolutionLogID] = @SegmentResolutionLogID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentResolutionLogDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

