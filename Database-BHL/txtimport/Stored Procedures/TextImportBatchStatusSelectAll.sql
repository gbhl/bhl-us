CREATE PROCEDURE txtimport.TextImportBatchStatusSelectAll

AS

BEGIN

SET NOCOUNT ON

SELECT	TextImportBatchStatusID,
		StatusName,
		StatusDescription
FROM	txtimport.TextImportBatchStatus

END

GO
