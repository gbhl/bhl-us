
-- NameSegmentUpdateAuto PROCEDURE
-- Generated 11/2/2012 3:47:04 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for NameSegment

CREATE PROCEDURE NameSegmentUpdateAuto

@NameSegmentID INT,
@NameID INT,
@SegmentID INT,
@NameSourceID INT,
@IsFirstOccurrence SMALLINT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[NameSegment]

SET

	[NameID] = @NameID,
	[SegmentID] = @SegmentID,
	[NameSourceID] = @NameSourceID,
	[IsFirstOccurrence] = @IsFirstOccurrence,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[NameSegmentID] = @NameSegmentID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure NameSegmentUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

