
-- SegmentAuthorUpdateAuto PROCEDURE
-- Generated 9/18/2012 12:12:30 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for SegmentAuthor

CREATE PROCEDURE SegmentAuthorUpdateAuto

@SegmentAuthorID INT,
@SegmentID INT,
@AuthorID INT,
@SequenceOrder SMALLINT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[SegmentAuthor]

SET

	[SegmentID] = @SegmentID,
	[AuthorID] = @AuthorID,
	[SequenceOrder] = @SequenceOrder,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[SegmentAuthorID] = @SegmentAuthorID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentAuthorUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[SegmentAuthorID],
		[SegmentID],
		[AuthorID],
		[SequenceOrder],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]

	FROM [dbo].[SegmentAuthor]
	
	WHERE
		[SegmentAuthorID] = @SegmentAuthorID
	
	RETURN -- update successful
END

