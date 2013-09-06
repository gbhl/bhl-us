
-- NameSegmentSelectAuto PROCEDURE
-- Generated 11/2/2012 3:47:04 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for NameSegment

CREATE PROCEDURE NameSegmentSelectAuto

@NameSegmentID INT

AS 

SET NOCOUNT ON

SELECT 

	[NameSegmentID],
	[NameID],
	[SegmentID],
	[NameSourceID],
	[IsFirstOccurrence],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]

FROM [dbo].[NameSegment]

WHERE
	[NameSegmentID] = @NameSegmentID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure NameSegmentSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

