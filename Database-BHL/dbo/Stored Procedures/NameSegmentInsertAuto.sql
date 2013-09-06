
-- NameSegmentInsertAuto PROCEDURE
-- Generated 11/2/2012 3:47:04 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for NameSegment

CREATE PROCEDURE NameSegmentInsertAuto

@NameSegmentID INT OUTPUT,
@NameID INT,
@SegmentID INT,
@NameSourceID INT,
@IsFirstOccurrence SMALLINT,
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[NameSegment]
(
	[NameID],
	[SegmentID],
	[NameSourceID],
	[IsFirstOccurrence],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@NameID,
	@SegmentID,
	@NameSourceID,
	@IsFirstOccurrence,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID
)

SET @NameSegmentID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure NameSegmentInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	
	RETURN -- insert successful
END

