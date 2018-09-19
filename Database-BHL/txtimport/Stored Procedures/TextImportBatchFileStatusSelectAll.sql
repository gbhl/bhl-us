CREATE PROCEDURE [txtimport].[TextImportBatchFileStatusSelectAll]

AS

BEGIN

SET NOCOUNT ON

SELECT	TextImportBatchFileStatusID,
		StatusName,
		StatusDescription
FROM	txtimport.TextImportBatchFileStatus

END
