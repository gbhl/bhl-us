CREATE PROCEDURE [txtimport].[TextImportBatchUpdateStatus]

@TextImportBatchID int,
@TextImportBatchStatusID int

AS

BEGIN

SET NOCOUNT ON

UPDATE	txtimport.TextImportBatch
SET		TextImportBatchStatusID = @TextImportBatchStatusID
WHERE	TextImportBatchID = @TextImportBatchID

END
