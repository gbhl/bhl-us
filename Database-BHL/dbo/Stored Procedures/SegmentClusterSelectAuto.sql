-- SegmentClusterSelectAuto PROCEDURE
-- Generated 9/20/2013 4:40:05 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for SegmentCluster

CREATE PROCEDURE SegmentClusterSelectAuto

@SegmentClusterID INT

AS 

SET NOCOUNT ON

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

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentClusterSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


