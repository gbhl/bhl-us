
CREATE PROCEDURE [dbo].[VaultSelectAll]
AS 

SET NOCOUNT ON

SELECT 
	[VaultID],
	[Server],
	[FolderShare],
	[WebVirtualDirectory],
	[OCRFolderShare]
FROM [dbo].[Vault]
order by Server, FolderShare
