CREATE PROCEDURE [import].[ImportFileStatusSelectAll]

AS

BEGIN

SET NOCOUNT ON

SELECT	ImportFileStatusID,
		StatusName,
		StatusDescription
FROM	import.ImportFileStatus

END