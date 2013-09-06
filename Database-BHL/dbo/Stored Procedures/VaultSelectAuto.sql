
-- VaultSelectAuto PROCEDURE
-- Generated 1/24/2008 10:03:58 AM
-- Do not modify the contents of this procedure.
-- Select Procedure for Vault

CREATE PROCEDURE VaultSelectAuto

@VaultID INT /* Unique identifier for each Vault entry. */

AS 

SET NOCOUNT ON

SELECT 

	[VaultID],
	[Server],
	[FolderShare],
	[WebVirtualDirectory],
	[OCRFolderShare]

FROM [dbo].[Vault]

WHERE
	[VaultID] = @VaultID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure VaultSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

