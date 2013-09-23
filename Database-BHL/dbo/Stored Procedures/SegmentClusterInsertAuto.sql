
-- SegmentClusterInsertAuto PROCEDURE
-- Generated 9/20/2013 4:40:05 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for SegmentCluster

CREATE PROCEDURE SegmentClusterInsertAuto

@SegmentClusterID INT OUTPUT,
@CreationUserID INT,
@LastModifiedUserID INT,
@SegmentClusterTypeID INT

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[SegmentCluster]
(
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID],
	[SegmentClusterTypeID]
)
VALUES
(
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID,
	@SegmentClusterTypeID
)

SET @SegmentClusterID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentClusterInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[SegmentClusterID],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID],
		[SegmentClusterTypeID]	

	FROM [dbo].[SegmentCluster]
	
	WHERE
		[SegmentClusterID] = @SegmentClusterID
	
	RETURN -- insert successful
END
