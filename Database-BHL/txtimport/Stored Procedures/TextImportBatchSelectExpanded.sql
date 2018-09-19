CREATE PROCEDURE txtimport.TextImportBatchSelectExpanded

@TextImportBatchID int

AS

BEGIN

SET NOCOUNT ON

SELECT	b.TextImportBatchID,
		b.TextImportBatchStatusID,
		s.StatusName,
		b.CreationDate,
		b.CreationUserID,
		u.FirstName + ' ' + u.LastName AS CreationUser
FROM	txtimport.TextImportBatch b
		INNER JOIN txtimport.TextImportBatchStatus s ON b.TextImportBatchStatusID = s.TextImportBatchStatusID
		INNER JOIN dbo.AspNetUsers u ON b.CreationUserID = u.ID
WHERE	TextImportBatchID = @TextImportBatchID

END

