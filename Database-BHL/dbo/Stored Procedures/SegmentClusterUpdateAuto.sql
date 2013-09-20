﻿
-- SegmentClusterUpdateAuto PROCEDURE
-- Generated 9/20/2013 4:40:05 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for SegmentCluster

CREATE PROCEDURE SegmentClusterUpdateAuto

@SegmentClusterID INT,
@LastModifiedUserID INT,
@SegmentClusterTypeID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[SegmentCluster]

SET

	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID,
	[SegmentClusterTypeID] = @SegmentClusterTypeID

WHERE
	[SegmentClusterID] = @SegmentClusterID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentClusterUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

