CREATE PROCEDURE [dbo].[InstitutionGroupInstitutionsInsertList]

@InstitutionGroupID int,
@Codes dbo.InstitutionCodeTable READONLY

AS 

SET NOCOUNT ON

BEGIN TRY

	BEGIN TRAN

	-- Delete Institutions not in the list
	DELETE	igi
	FROM	dbo.InstitutionGroupInstitution igi
			LEFT JOIN @Codes c ON igi.InstitutionCode = c.Code
	WHERE	igi.InstitutionGroupID = @InstitutionGroupID
	AND		c.Code IS NULL

	-- Institutions to add
	INSERT	dbo.InstitutionGroupInstitution (InstitutionGroupID, InstitutionCode)
	SELECT	@InstitutionGroupID, c.Code
	FROM	@Codes c
			LEFT JOIN dbo.InstitutionGroupInstitution igi ON c.Code = igi.InstitutionCode AND igi.InstitutionGroupID = @InstitutionGroupID
	WHERE	igi.InstitutionGroupInstitutionID IS NULL

	COMMIT TRAN
END TRY
BEGIN CATCH
	DECLARE @ErrMsg nvarchar(350)
	SET @ErrMsg = 'Error updating institutions for group ' + CONVERT(nvarchar(20), @InstitutionGroupID)

	DECLARE @ErrSeverity INT
	DECLARE @ErrState INT	
	SELECT	@ErrSeverity = ERROR_SEVERITY(), @ErrState = ERROR_STATE()

	IF @@TRANCOUNT > 0 ROLLBACK TRAN

	RAISERROR (@ErrMsg, @ErrSeverity, @ErrState)
END CATCH

