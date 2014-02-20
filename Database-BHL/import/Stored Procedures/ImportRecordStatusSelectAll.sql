CREATE PROCEDURE import.ImportRecordStatusSelectAll

AS

BEGIN

SET NOCOUNT ON

SELECT	ImportRecordStatusID,
		StatusName,
		StatusDescription
FROM	import.ImportRecordStatus

END