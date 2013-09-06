
-- SegmentKeywordInsertAuto PROCEDURE
-- Generated 9/18/2012 12:12:30 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for SegmentKeyword

CREATE PROCEDURE SegmentKeywordInsertAuto

@SegmentKeywordID INT OUTPUT,
@SegmentID INT,
@KeywordID INT,
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[SegmentKeyword]
(
	[SegmentID],
	[KeywordID],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@SegmentID,
	@KeywordID,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID
)

SET @SegmentKeywordID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentKeywordInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

