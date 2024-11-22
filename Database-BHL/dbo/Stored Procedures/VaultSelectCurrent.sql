CREATE PROCEDURE dbo.VaultSelectCurrent

AS

BEGIN

SET NOCOUNT ON 

SELECT	VaultID,
		Server,
		FolderShare,
		WebVirtualDirectory,
		OCRFolderShare
FROM	dbo.Vault
WHERE	IsCurrent = 1

END
GO
