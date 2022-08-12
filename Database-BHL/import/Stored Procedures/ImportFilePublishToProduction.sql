CREATE PROCEDURE [import].[ImportFilePublishToProduction]

@ImportFileID int,
@UserID int

AS

BEGIN

SET NOCOUNT ON

BEGIN TRY
	-- --------------------
	-- Create author records in production for any new authors
	exec import.ImportRecordCreatorPublishToProduction @ImportFileID, @UserID

	-- Get status IDs
	DECLARE @ImportFileImportedID int
	SELECT @ImportFileImportedID = ImportFileStatusID FROM import.ImportFileStatus WHERE StatusName = 'Imported'
	IF (@ImportFileImportedID IS NULL) RAISERROR('ImportFileStatus -Imported- not found', 0, 1)

	DECLARE @ImportRecordNewID int
	SELECT @ImportRecordNewID = ImportRecordStatusID FROM import.ImportRecordStatus WHERE StatusName = 'OK'
	IF (@ImportRecordNewID IS NULL) RAISERROR('ImportRecordStatus -OK- not found', 0, 1)

	-- --------------------
	-- Create Title/Item/Segment/Creator/Keyword records for each imported record
	DECLARE @ImportRecordID int

	-- Get the IDs of the imported records to be added
	DECLARE	curAddRecord CURSOR 
	FOR		SELECT	ImportRecordID 
			FROM	import.ImportRecord 
			WHERE	ImportFileID = @ImportFileID 
			AND		ImportRecordStatusID = @ImportRecordNewID
			AND		ImportSegmentID IS NULL
			ORDER BY ImportRecordID
	
	OPEN curAddRecord
	FETCH NEXT FROM curAddRecord INTO @ImportRecordID

	-- Publish each record
	WHILE (@@fetch_status <> -1)
	BEGIN
		IF (@@fetch_status <> -2)
		BEGIN
			exec import.ImportRecordPublishToProduction @ImportRecordID, @UserID
		END
		FETCH NEXT FROM curAddRecord INTO @ImportRecordID
	END

	CLOSE curAddRecord
	DEALLOCATE curAddRecord

	-- --------------------
	-- Get the IDs of the imported records to be updated
	DECLARE	curUpdateRecord CURSOR 
	FOR		SELECT	ImportRecordID 
			FROM	import.ImportRecord 
			WHERE	ImportFileID = @ImportFileID 
			AND		ImportRecordStatusID = @ImportRecordNewID
			AND		ImportSegmentID IS NOT NULL
			ORDER BY ImportRecordID
	
	OPEN curUpdateRecord
	FETCH NEXT FROM curUpdateRecord INTO @ImportRecordID

	-- Publish each record
	WHILE (@@fetch_status <> -1)
	BEGIN
		IF (@@fetch_status <> -2)
		BEGIN
			exec import.ImportRecordUpdateProduction @ImportRecordID, @UserID
		END
		FETCH NEXT FROM curUpdateRecord INTO @ImportRecordID
	END

	CLOSE curUpdateRecord
	DEALLOCATE curUpdateRecord

	-- --------------------
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
