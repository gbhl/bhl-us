
CREATE PROCEDURE VaultInsertAuto

@VaultID INT,
@Server NVARCHAR(30) = null,
@FolderShare NVARCHAR(30) = null,
@WebVirtualDirectory NVARCHAR(30) = null,
@OCRFolderShare NVARCHAR(100) = null,
@IsCurrent TINYINT

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Vault]
( 	[VaultID],
	[Server],
	[FolderShare],
	[WebVirtualDirectory],
	[OCRFolderShare],
	[IsCurrent] )
VALUES
( 	@VaultID,
	@Server,
	@FolderShare,
	@WebVirtualDirectory,
	@OCRFolderShare,
	@IsCurrent )

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.VaultInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END
GO
