CREATE PROCEDURE [reqlog].[DailyTasks]

AS 

SET NOCOUNT ON

BEGIN TRY

exec reqlog.RequestLogArchiveDeleteOld
exec reqlog.RequestLogMoveRecordsToArchive
exec reqlog.RequestLogMoveRecordsToHistory

END TRY
BEGIN CATCH
	DECLARE @ErrorMessage NVARCHAR(4000)
    DECLARE @ErrorSeverity INT
    DECLARE @ErrorState INT

    SELECT	@ErrorMessage = ERROR_MESSAGE(),
			@ErrorSeverity = ERROR_SEVERITY(),
			@ErrorState = ERROR_STATE()

    RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState)
END CATCH
