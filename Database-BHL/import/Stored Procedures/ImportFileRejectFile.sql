CREATE PROCEDURE [import].[ImportFileRejectFile]

@ImportFileID int,
@UserID int

AS

BEGIN

SET NOCOUNT ON

BEGIN TRY
	DECLARE @ImportFileRejectedID int
	SELECT @ImportFileRejectedID = ImportFileStatusID FROM import.ImportFileStatus WHERE StatusName = 'Rejected'
	IF (@ImportFileRejectedID IS NULL) RAISERROR('ImportFileStatus -Rejected- not found', 0, 1)

	DECLARE @ImportRecordNewID int
	SELECT @ImportRecordNewID = ImportRecordStatusID FROM import.ImportRecordStatus WHERE StatusName = 'OK'
	IF (@ImportRecordNewID IS NULL) RAISERROR('ImportRecordStatus -OK- not found', 0, 1)

	DECLARE @ImportRecordRejectedID int
	SELECT @ImportRecordRejectedID = ImportRecordStatusID FROM import.ImportRecordStatus WHERE StatusName = 'Rejected'
	IF (@ImportRecordRejectedID IS NULL) RAISERROR('ImportRecordStatus -Rejected- not found', 0, 1)

	BEGIN TRAN

	-- Set the file to "Rejected"
	UPDATE	import.ImportFile 
	SET		ImportFileStatusID = @ImportFileRejectedID,
			LastModifiedDate = GETDATE(),
			LastModifiedUserID = @UserID
	WHERE	ImportFileID = @ImportFileID

	-- Set all "OK" records in the file to "Rejected"
	UPDATE	import.ImportRecord
	SET		ImportRecordStatusID = @ImportRecordRejectedID,
			LastModifiedDate = GETDATE(),
			LastModifiedUserID = @UserID
	WHERE	ImportFileID = @ImportFileID
	AND		ImportRecordStatusID = @ImportRecordNewID

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

GO
