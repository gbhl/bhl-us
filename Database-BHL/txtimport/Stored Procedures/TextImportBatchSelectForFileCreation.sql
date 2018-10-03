CREATE PROCEDURE [txtimport].[TextImportBatchSelectForFileCreation]
AS
BEGIN
/*
 * Because it affects the status of the selected Text Import batch, this procedure should 
 * only be called by the Text Import Processor (which will reset the status when it has
 * processed the batch).
 */

SET NOCOUNT ON

DECLARE @TextImportBatchID int
DECLARE @TextImportStatusQueued int
DECLARE @TextImportStatusProcessing int

SELECT	@TextImportStatusQueued = TextImportBatchStatusID
FROM	txtimport.TextImportBatchStatus
WHERE	StatusName = 'Queued'

SELECT	@TextImportStatusProcessing = TextImportBatchStatusID
FROM	txtimport.TextImportBatchStatus
WHERE	StatusName = 'Processing'

-- Get the identifier of the next batch to be processed
SELECT	@TextImportBatchID = MIN(TextImportBatchID)
FROM	txtimport.TextImportBatch
WHERE	TextImportBatchStatusID = @TextImportStatusQueued

-- Update the status of the batch request to "Processing"
UPDATE	txtimport.TextImportBatch
SET		TextImportBatchStatusID = @TextImportStatusProcessing,
		LastModifiedDate = GETDATE()
WHERE	TextImportBatchID = @TextImportBatchID
AND		TextImportBatchStatusID = @TextImportStatusQueued

-- If no status update was made, then clear out the selected batch request.
-- This ensures that only one generator process will handle the request (the
-- one that is able to update the status to "Processing").
IF (@@ROWCOUNT = 0) SET @TextImportBatchID = 0

-- Return the details of the selected batch
SELECT	TextImportBatchID,
		TextImportBatchStatusID,
		CreationDate,
		LastModifiedDate,
		CreationUserID,
		LastModifiedUserID
FROM	txtimport.TextImportBatch
WHERE	TextImportBatchID = @TextImportBatchID

END
