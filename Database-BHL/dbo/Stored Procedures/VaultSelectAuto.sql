CREATE PROCEDURE VaultSelectAuto

@VaultID INT

AS 

SET NOCOUNT ON

SELECT	
	[VaultID],
	[Server],
	[FolderShare],
	[WebVirtualDirectory],
	[OCRFolderShare],
	[IsCurrent]
FROM	
	[dbo].[Vault]
WHERE	
	[VaultID] = @VaultID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.VaultSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
