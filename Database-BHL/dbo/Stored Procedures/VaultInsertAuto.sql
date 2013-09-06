
-- VaultInsertAuto PROCEDURE
-- Generated 1/24/2008 10:03:58 AM
-- Do not modify the contents of this procedure.
-- Insert Procedure for Vault

CREATE PROCEDURE VaultInsertAuto

@VaultID INT /* Unique identifier for each Vault entry. */,
@Server NVARCHAR(30) = null /* Name of server for this Vault entry. */,
@FolderShare NVARCHAR(30) = null /* Name for the folder share for this Vault entry. */,
@WebVirtualDirectory NVARCHAR(30) = null /* Name for the Web Virtual Directory for this Vault entry. */,
@OCRFolderShare NVARCHAR(100) = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Vault]
(
	[VaultID],
	[Server],
	[FolderShare],
	[WebVirtualDirectory],
	[OCRFolderShare]
)
VALUES
(
	@VaultID,
	@Server,
	@FolderShare,
	@WebVirtualDirectory,
	@OCRFolderShare
)

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure VaultInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

