
-- SegmentStatusUpdateAuto PROCEDURE
-- Generated 9/18/2012 12:12:30 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for SegmentStatus

CREATE PROCEDURE SegmentStatusUpdateAuto

@SegmentStatusID INT,
@StatusName NVARCHAR(50),
@StatusDescription NVARCHAR(500),
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[SegmentStatus]

SET

	[SegmentStatusID] = @SegmentStatusID,
	[StatusName] = @StatusName,
	[StatusDescription] = @StatusDescription,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[SegmentStatusID] = @SegmentStatusID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentStatusUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[SegmentStatusID],
		[StatusName],
		[StatusDescription],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]

	FROM [dbo].[SegmentStatus]
	
	WHERE
		[SegmentStatusID] = @SegmentStatusID
	
	RETURN -- update successful
END

