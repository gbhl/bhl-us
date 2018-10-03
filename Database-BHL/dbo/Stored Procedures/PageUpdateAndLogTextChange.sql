CREATE PROCEDURE dbo.PageUpdateAndLogTextChange

@PageID int,
@TextSource nvarchar(50),
@BatchID int,
@UserID int

AS

BEGIN

SET NOCOUNT ON

DECLARE @UpdateDate DATETIME
SET @UpdateDate = GETDATE()

BEGIN TRY

	BEGIN TRAN

	UPDATE	dbo.Page
	SET		LastModifiedDate = @UpdateDate,
			LastModifiedUserID = @UserID
	WHERE	PageID = @PageID

	INSERT	dbo.PageTextLog (PageID, TextSource, TextImportBatchFileID, CreationDate, CreationUserID)
	VALUES (@PageID, @TextSource, @BatchID, @UpdateDate, @UserID)

	COMMIT TRAN

END TRY
BEGIN CATCH
	DECLARE @ErrorMsg nvarchar(max), @Severity int, @State int
	SELECT @ErrorMsg = ERROR_MESSAGE(), @Severity = ERROR_SEVERITY(), @State = ERROR_STATE()
	IF @@TRANCOUNT > 0 ROLLBACK TRAN
	RAISERROR(@ErrorMsg, @Severity, @State)
END CATCH

END
