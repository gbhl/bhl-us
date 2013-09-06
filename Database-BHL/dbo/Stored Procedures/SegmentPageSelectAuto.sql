
-- SegmentPageSelectAuto PROCEDURE
-- Generated 9/18/2012 12:12:30 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for SegmentPage

CREATE PROCEDURE SegmentPageSelectAuto

@SegmentPageID INT

AS 

SET NOCOUNT ON

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

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentPageSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

