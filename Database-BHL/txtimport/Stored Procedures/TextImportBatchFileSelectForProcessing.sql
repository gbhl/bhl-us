CREATE PROCEDURE txtimport.TextImportBatchFileSelectForProcessing

@TextImportBatchID int

AS

BEGIN

SET NOCOUNT ON

DECLARE @TextImportFileStatusReady int

SELECT	@TextImportFileStatusReady = TextImportBatchFileStatusID
FROM	txtimport.TextImportBatchFileStatus
WHERE	StatusName = 'Ready to Import'

SELECT	TextImportBatchFileID,
		TextImportBatchID,
		TextImportBatchFileStatusID,
		ItemID,
		[Filename],
		FileFormat,
		ErrorMessage,
		CreationDate,
		LastModifiedDate,
		CreationUserID,
		LastModifiedUserID
FROM	txtimport.TextImportBatchFile
WHERE	TextImportBatchID = @TextImportBatchID
AND		TextImportBatchFileStatusID = @TextImportFileStatusReady

END
