CREATE PROCEDURE [import].[ImportFilePublishToProduction]

@ImportFileID int,
@UserID int

AS

BEGIN

SET NOCOUNT ON

BEGIN TRY
	-- Get status IDs
	DECLARE @ImportFileImportedID int
	SELECT @ImportFileImportedID = ImportFileStatusID FROM import.ImportFileStatus WHERE StatusName = 'Imported'
	IF (@ImportFileImportedID IS NULL) RAISERROR('ImportFileStatus -Imported- not found', 0, 1)

	DECLARE @ImportRecordNewID int
	SELECT @ImportRecordNewID = ImportRecordStatusID FROM import.ImportRecordStatus WHERE StatusName = 'New'
	IF (@ImportRecordNewID IS NULL) RAISERROR('ImportRecordStatus -New- not found', 0, 1)

	-- Create Title/Item/Segment/Creator/Keyword records for each imported record
	DECLARE @ImportRecordID int

	-- Get the IDs of the records to be imported
	DECLARE	curRecord CURSOR 
	FOR SELECT ImportRecordID FROM import.ImportRecord WHERE ImportFileID = @ImportFileID AND ImportRecordStatusID = @ImportRecordNewID
	
	OPEN curRecord
	FETCH NEXT FROM curRecord INTO @ImportRecordID

	-- Publish each record
	WHILE (@@fetch_status <> -1)
	BEGIN
		IF (@@fetch_status <> -2)
		BEGIN
			exec import.ImportRecordPublishToProduction @ImportRecordID, @UserID
		END
		FETCH NEXT FROM curRecord INTO @ImportRecordID
	END

	CLOSE curRecord
	DEALLOCATE curRecord

	-- Set the file to "Imported"
	UPDATE	import.ImportFile 
	SET		ImportFileStatusID = @ImportFileImportedID,
			LastModifiedDate = GETDATE(),
			LastModifiedUserID = @UserID
	WHERE	ImportFileID = @ImportFileID

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

