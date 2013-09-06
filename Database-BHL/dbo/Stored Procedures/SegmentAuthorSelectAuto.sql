
-- SegmentAuthorSelectAuto PROCEDURE
-- Generated 9/18/2012 12:12:30 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for SegmentAuthor

CREATE PROCEDURE SegmentAuthorSelectAuto

@SegmentAuthorID INT

AS 

SET NOCOUNT ON

SELECT 

	[SegmentAuthorID],
	[SegmentID],
	[AuthorID],
	[SequenceOrder],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]

FROM [dbo].[SegmentAuthor]

WHERE
	[SegmentAuthorID] = @SegmentAuthorID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentAuthorSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

