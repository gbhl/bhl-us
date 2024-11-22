CREATE PROCEDURE VaultUpdateAuto

@VaultID INT,
@Server NVARCHAR(30),
@FolderShare NVARCHAR(30),
@WebVirtualDirectory NVARCHAR(30),
@OCRFolderShare NVARCHAR(100),
@IsCurrent TINYINT

AS 

SET NOCOUNT ON

UPDATE [dbo].[Vault]
SET
	[VaultID] = @VaultID,
	[Server] = @Server,
	[FolderShare] = @FolderShare,
	[WebVirtualDirectory] = @WebVirtualDirectory,
	[OCRFolderShare] = @OCRFolderShare,
	[IsCurrent] = @IsCurrent
WHERE
	[VaultID] = @VaultID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.VaultUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[VaultID],
		[Server],
		[FolderShare],
		[WebVirtualDirectory],
		[OCRFolderShare],
		[IsCurrent]
	FROM [dbo].[Vault]
	WHERE
		[VaultID] = @VaultID
	
	RETURN -- update successful
END
GO
