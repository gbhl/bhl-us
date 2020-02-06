CREATE PROCEDURE dbo.IASegmentPageInsertAuto

@SegmentPageID INT OUTPUT,
@SegmentID INT,
@PageSequence INT

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[IASegmentPage]
( 	[SegmentID],
	[PageSequence],
	[CreatedDate],
	[LastModifiedDate] )
VALUES
( 	@SegmentID,
	@PageSequence,
	getdate(),
	getdate() )

SET @SegmentPageID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.IASegmentPageInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[SegmentPageID],
		[SegmentID],
		[PageSequence],
		[CreatedDate],
		[LastModifiedDate]	
	FROM [dbo].[IASegmentPage]
	WHERE
		[SegmentPageID] = @SegmentPageID
	
	RETURN -- insert successful
END
