
-- SegmentKeywordUpdateAuto PROCEDURE
-- Generated 9/18/2012 12:12:30 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for SegmentKeyword

CREATE PROCEDURE SegmentKeywordUpdateAuto

@SegmentKeywordID INT,
@SegmentID INT,
@KeywordID INT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[SegmentKeyword]

SET

	[SegmentID] = @SegmentID,
	[KeywordID] = @KeywordID,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[SegmentKeywordID] = @SegmentKeywordID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentKeywordUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	
	RETURN -- update successful
END

