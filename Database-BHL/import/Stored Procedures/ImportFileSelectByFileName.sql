CREATE PROCEDURE import.ImportFileSelectByFileName

@ImportFileName nvarchar(200)

AS

BEGIN

SET NOCOUNT ON

SELECT	ImportFileID,
		ImportFileStatusID,
		ImportFileName,
		ContributorCode
FROM	import.ImportFile
WHERE	ImportFileName = @ImportFileName

END
