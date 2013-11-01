CREATE PROCEDURE dbo.OAIRecordDeleteForHarvestLogID

@HarvestLogID int

AS

BEGIN

SET NOCOUNT ON

BEGIN TRY
	BEGIN TRAN

	DELETE	dbo.OAIRecordCreator 
	FROM	dbo.OAIRecordCreator c INNER JOIN dbo.OAIRecord r on c.OAIRecordID = r.OAIRecordiD
	WHERE	r.HarvestLogID = @HarvestLogID

	DELETE	dbo.OAIRecordDCType 
	FROM	dbo.OAIRecordDCType d INNER JOIN dbo.OAIRecord r on d.OAIRecordID = r.OAIRecordiD
	WHERE	r.HarvestLogID = @HarvestLogID

	DELETE	dbo.OAIRecordRight 
	FROM	dbo.OAIRecordRight rt INNER JOIN dbo.OAIRecord r on rt.OAIRecordID = r.OAIRecordiD
	WHERE	r.HarvestLogID = @HarvestLogID

	DELETE	dbo.OAIRecordSubject 
	FROM	dbo.OAIRecordSubject s INNER JOIN dbo.OAIRecord r on s.OAIRecordID = r.OAIRecordiD
	WHERE	r.HarvestLogID = @HarvestLogID

	DELETE dbo.OAIRecord WHERE HarvestLogID = @HarvestLogID

	COMMIT TRAN

END TRY
BEGIN CATCH

	IF (@@TRANCOUNT > 0) ROLLBACK TRAN
	THROW

END CATCH

END

GO
