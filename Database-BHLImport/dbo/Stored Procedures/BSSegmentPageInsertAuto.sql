CREATE PROCEDURE dbo.BSSegmentPageInsertAuto

@SegmentPageID INT OUTPUT,
@SegmentID INT,
@BHLPageID INT,
@SequenceOrder SMALLINT

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[BSSegmentPage]
( 	[SegmentID],
	[BHLPageID],
	[SequenceOrder],
	[CreationDate] )
VALUES
( 	@SegmentID,
	@BHLPageID,
	@SequenceOrder,
	getdate() )

SET @SegmentPageID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.BSSegmentPageInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[SegmentPageID],
		[SegmentID],
		[BHLPageID],
		[SequenceOrder],
		[CreationDate]	
	FROM [dbo].[BSSegmentPage]
	WHERE
		[SegmentPageID] = @SegmentPageID
	
	RETURN -- insert successful
END
GO
