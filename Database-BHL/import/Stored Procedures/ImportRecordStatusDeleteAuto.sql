CREATE PROCEDURE [import].[ImportRecordStatusDeleteAuto]

@ImportRecordStatusID INT

AS 

DELETE FROM [import].[ImportRecordStatus]

WHERE

	[ImportRecordStatusID] = @ImportRecordStatusID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ImportRecordStatusDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END


GO
