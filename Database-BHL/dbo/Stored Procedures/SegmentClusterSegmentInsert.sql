CREATE PROCEDURE [dbo].[SegmentClusterSegmentInsert]

@SegmentID int,
@SegmentClusterID int,
@IsPrimary smallint,
@SegmentClusterTypeID int,
@UserID int

AS

BEGIN

SET NOCOUNT ON

IF (@SegmentClusterID IS NULL)
BEGIN
	INSERT dbo.SegmentCluster 
		(SegmentClusterTypeID, CreationUserID, LastModifiedUserID) 
	VALUES (@SegmentClusterTypeID, @UserID, @UserID)
		
	SELECT @SegmentClusterID = Scope_Identity()
END

INSERT dbo.SegmentClusterSegment 
	(SegmentID, SegmentClusterID, IsPrimary, CreationUserID, LastModifiedUserID)
VALUES (@SegmentID, @SegmentClusterID, ISNULL(@IsPrimary, 0), @UserID, @UserID)

-- Return the newly inserted/updated record
SELECT	SegmentID,
		SegmentClusterID,
		IsPrimary,
		CreationDate,
		LastModifiedDate,
		CreationUserID,
		LastModifiedUserID
FROM	dbo.SegmentClusterSegment
WHERE	SegmentID = @SegmentID
AND		SegmentClusterID = @SegmentClusterID
	
END

