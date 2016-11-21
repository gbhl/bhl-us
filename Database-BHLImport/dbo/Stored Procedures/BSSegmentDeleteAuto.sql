-- Delete Procedure for dbo.BSSegment
-- Do not modify the contents of this procedure.
-- Generated 11/21/2016 1:39:33 PM

CREATE PROCEDURE [dbo].[BSSegmentDeleteAuto]

@SegmentID INT

AS 

SET NOCOUNT ON

DELETE 
FROM	
	[dbo].[BSSegment]
WHERE	
	[SegmentID] = @SegmentID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.BSSegmentDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END
