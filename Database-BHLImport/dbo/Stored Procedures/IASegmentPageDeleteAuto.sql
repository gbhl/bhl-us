CREATE PROCEDURE dbo.IASegmentPageDeleteAuto

@SegmentPageID INT

AS 

SET NOCOUNT ON

DELETE 
FROM	
	[dbo].[IASegmentPage]
WHERE	
	[SegmentPageID] = @SegmentPageID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.IASegmentPageDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END
