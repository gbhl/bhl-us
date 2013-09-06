
-- SegmentAuthorInsertAuto PROCEDURE
-- Generated 9/18/2012 12:12:30 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for SegmentAuthor

CREATE PROCEDURE SegmentAuthorInsertAuto

@SegmentAuthorID INT OUTPUT,
@SegmentID INT,
@AuthorID INT,
@SequenceOrder SMALLINT,
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[SegmentAuthor]
(
	[SegmentID],
	[AuthorID],
	[SequenceOrder],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@SegmentID,
	@AuthorID,
	@SequenceOrder,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID
)

SET @SegmentAuthorID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentAuthorInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	
	RETURN -- insert successful
END

