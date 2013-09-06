
-- SegmentClusterSegmentInsertAuto PROCEDURE
-- Generated 9/18/2012 12:12:30 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for SegmentClusterSegment

CREATE PROCEDURE SegmentClusterSegmentInsertAuto

@SegmentID INT,
@SegmentClusterID INT,
@IsPrimary SMALLINT,
@CreationUserID INT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[SegmentClusterSegment]
(
	[SegmentID],
	[SegmentClusterID],
	[IsPrimary],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@SegmentID,
	@SegmentClusterID,
	@IsPrimary,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID
)

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentClusterSegmentInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[SegmentID],
		[SegmentClusterID],
		[IsPrimary],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]	

	FROM [dbo].[SegmentClusterSegment]
	
	WHERE
		[SegmentID] = @SegmentID
	
	RETURN -- insert successful
END

