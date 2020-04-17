CREATE PROCEDURE dbo.InstitutionGroupDelete

@InstitutionGroupID int

AS

BEGIN

SET NOCOUNT ON

BEGIN TRY
	BEGIN TRAN

	DELETE FROM dbo.InstitutionGroupInstitution WHERE InstitutionGroupID = @InstitutionGroupID
	DELETE FROM dbo.InstitutionGroup WHERE InstitutionGroupID = @InstitutionGroupID

	COMMIT TRAN
END TRY
BEGIN CATCH
	DECLARE @ErrorMsg nvarchar(max), @Severity int, @State int
	SELECT @ErrorMsg = ERROR_MESSAGE(), @Severity = ERROR_SEVERITY(), @State = ERROR_STATE()
	IF @@TRANCOUNT > 0 ROLLBACK TRAN
	RAISERROR(@ErrorMsg, @Severity, @State)
END CATCH

END
