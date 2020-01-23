CREATE PROCEDURE [dbo].[BSItemDeleteAllSegments]

@ItemID int

AS

BEGIN

SET NOCOUNT ON

BEGIN TRY

	DECLARE @ImportSourceID int
	SELECT @ImportSourceID = ImportSourceID FROM dbo.ImportSource WHERE Source = 'BioStor'

	BEGIN TRAN

	DELETE FROM dbo.BSSegmentPage 
	WHERE	SegmentID IN (SELECT SegmentID FROM dbo.BSSegment WHERE ItemID = @ItemID)
	
	DELETE FROM dbo.BSSegmentAuthor 
	WHERE	SegmentID IN (SELECT SegmentID FROM dbo.BSSegment WHERE ItemID = @ItemID)
	AND		ImportSourceID = @ImportSourceID
	
	DELETE FROM dbo.BSSegment 
	WHERE	ItemID = @ItemID

	COMMIT TRAN
END TRY
BEGIN CATCH
	DECLARE @ErrMsg NVARCHAR(4000)
	DECLARE @ErrSeverity INT
	DECLARE @ErrState INT
	
	SELECT	@ErrMsg = ERROR_MESSAGE(),
			@ErrSeverity = ERROR_SEVERITY(),
			@ErrState = ERROR_STATE()

	IF @@TRANCOUNT > 0 ROLLBACK TRAN

	RAISERROR (@ErrMsg, @ErrSeverity, @ErrState)
END CATCH

END
