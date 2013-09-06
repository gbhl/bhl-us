
-- SegmentStatusInsertAuto PROCEDURE
-- Generated 9/18/2012 12:12:30 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for SegmentStatus

CREATE PROCEDURE SegmentStatusInsertAuto

@SegmentStatusID INT,
@StatusName NVARCHAR(50),
@StatusDescription NVARCHAR(500),
@CreationUserID INT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[SegmentStatus]
(
	[SegmentStatusID],
	[StatusName],
	[StatusDescription],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@SegmentStatusID,
	@StatusName,
	@StatusDescription,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID
)

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentStatusInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

