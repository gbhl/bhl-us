create PROCEDURE [dbo].[VaultSelectMaxID]
AS 

SET NOCOUNT ON

SELECT 
MAX(	[VaultID] )
FROM [dbo].[Vault]
