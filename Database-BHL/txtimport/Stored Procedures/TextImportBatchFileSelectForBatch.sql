CREATE PROCEDURE [txtimport].[TextImportBatchFileSelectForBatch]

@TextImportBatchID int

AS

BEGIN

SET NOCOUNT ON

SELECT	f.TextImportBatchFileID,
		f.TextImportBatchFileStatusID,
		s.StatusName,
		f.ItemID,
		f.[Filename],
		f.FileFormat,
		f.ErrorMessage
FROM	txtimport.TextImportBatchFile f
		INNER JOIN txtimport.TextImportBatchFileStatus s ON f.TextImportBatchFileStatusID = s.TextImportBatchFileStatusID
WHERE	f.TextImportBatchID = @TextImportBatchID

END