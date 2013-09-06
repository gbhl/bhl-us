
-- SegmentKeywordSelectAuto PROCEDURE
-- Generated 9/18/2012 12:12:30 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for SegmentKeyword

CREATE PROCEDURE SegmentKeywordSelectAuto

@SegmentKeywordID INT

AS 

SET NOCOUNT ON

SELECT 

	[SegmentKeywordID],
	[SegmentID],
	[KeywordID],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]

FROM [dbo].[SegmentKeyword]

WHERE
	[SegmentKeywordID] = @SegmentKeywordID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentKeywordSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

