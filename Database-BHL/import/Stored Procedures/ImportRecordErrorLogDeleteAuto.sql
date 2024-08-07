CREATE PROCEDURE import.ImportRecordErrorLogDeleteAuto

@ImportRecordErrorLogID INT

AS 

SET NOCOUNT ON

DELETE 
FROM	
	[import].[ImportRecordErrorLog]
WHERE	
	[ImportRecordErrorLogID] = @ImportRecordErrorLogID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure import.ImportRecordErrorLogDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END
