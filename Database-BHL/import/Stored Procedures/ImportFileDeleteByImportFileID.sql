CREATE PROCEDURE [import].[ImportFileDeleteByImportFileID]

@ImportFileID int

AS

BEGIN

SET NOCOUNT ON

BEGIN TRY
	BEGIN TRAN

	DELETE	import.ImportRecordPage
	FROM	import.ImportRecordPage p INNER JOIN import.ImportRecord r ON p.ImportRecordID = r.ImportRecordID
	WHERE	r.ImportFileID = @ImportFileID

	DELETE	import.ImportRecordCreator
	FROM	import.ImportRecordCreator c INNER JOIN import.ImportRecord r ON c.ImportRecordID = r.ImportRecordID
	WHERE	r.ImportFileID = @ImportFileID

	DELETE	import.ImportRecordKeyword
	FROM	import.ImportRecordKeyword k INNER JOIN import.ImportRecord r ON k.ImportRecordID = r.ImportRecordID
	WHERE	r.ImportFileID = @ImportFileID

	DELETE	import.ImportRecordErrorLog
	FROM	import.ImportRecordErrorLog l INNER JOIN import.ImportRecord r ON l.ImportRecordID = r.ImportRecordID
	WHERE	r.ImportFileID = @ImportFileID

	DELETE import.ImportRecord WHERE ImportFileID = @ImportFileID
	
	DELETE import.ImportFile WHERE ImportFileID = @ImportFileID

	COMMIT TRAN

END TRY
BEGIN CATCH

	IF (@@TRANCOUNT > 0) ROLLBACK TRAN
	THROW

END CATCH

END
