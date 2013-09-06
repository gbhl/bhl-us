
CREATE PROCEDURE [dbo].[SegmentClusterSegmentUpdate]

@SegmentID int,
@SegmentClusterID int,
@IsPrimary smallint,
@UserID int

AS

BEGIN

SET NOCOUNT ON

DECLARE @NumUpdated INT

UPDATE	dbo.SegmentClusterSegment
SET		SegmentClusterID = @SegmentClusterID,
		IsPrimary = ISNULL(@IsPrimary, 0)
WHERE	SegmentID = @SegmentID
AND		@SegmentClusterID IS NOT NULL

SELECT @NumUpdated = @@ROWCOUNT

IF (@NumUpdated = 0)
BEGIN
	IF (@SegmentClusterID IS NULL)
	BEGIN
		INSERT dbo.SegmentCluster 
			(CreationUserID, LastModifiedUserID) 
		VALUES (@UserID, @UserID)
		
		SELECT @SegmentClusterID = Scope_Identity()
	END

	INSERT dbo.SegmentClusterSegment 
		(SegmentID, SegmentClusterID, IsPrimary, CreationUserID, LastModifiedUserID)
	VALUES (@SegmentID, @SegmentClusterID, ISNULL(@IsPrimary, 0), @UserID, @UserID)
END

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
	
END

