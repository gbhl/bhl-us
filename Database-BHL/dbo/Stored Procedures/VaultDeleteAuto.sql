
-- VaultDeleteAuto PROCEDURE
-- Generated 1/24/2008 10:03:58 AM
-- Do not modify the contents of this procedure.
-- Delete Procedure for Vault

CREATE PROCEDURE VaultDeleteAuto

@VaultID INT /* Unique identifier for each Vault entry. */

AS 

DELETE FROM [dbo].[Vault]

WHERE

	[VaultID] = @VaultID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure VaultDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

