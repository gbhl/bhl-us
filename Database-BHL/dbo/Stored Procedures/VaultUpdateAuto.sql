
-- VaultUpdateAuto PROCEDURE
-- Generated 1/24/2008 10:03:58 AM
-- Do not modify the contents of this procedure.
-- Update Procedure for Vault

CREATE PROCEDURE VaultUpdateAuto

@VaultID INT /* Unique identifier for each Vault entry. */,
@Server NVARCHAR(30) /* Name of server for this Vault entry. */,
@FolderShare NVARCHAR(30) /* Name for the folder share for this Vault entry. */,
@WebVirtualDirectory NVARCHAR(30) /* Name for the Web Virtual Directory for this Vault entry. */,
@OCRFolderShare NVARCHAR(100)

AS 

SET NOCOUNT ON

UPDATE [dbo].[Vault]

SET

	[VaultID] = @VaultID,
	[Server] = @Server,
	[FolderShare] = @FolderShare,
	[WebVirtualDirectory] = @WebVirtualDirectory,
	[OCRFolderShare] = @OCRFolderShare

WHERE
	[VaultID] = @VaultID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure VaultUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[VaultID],
		[Server],
		[FolderShare],
		[WebVirtualDirectory],
		[OCRFolderShare]

	FROM [dbo].[Vault]
	
	WHERE
		[VaultID] = @VaultID
	
	RETURN -- update successful
END

