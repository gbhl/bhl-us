﻿CREATE PROCEDURE dbo.OAIRecordDeleteForOAIRecordID

@OAIRecordID int

AS

BEGIN

SET NOCOUNT ON

BEGIN TRY
	BEGIN TRAN

	DELETE dbo.OAIRecordRelatedTitle WHERE OAIRecordID = @OAIRecordID
	DELETE dbo.OAIRecordCreator WHERE OAIRecordID = @OAIRecordID
	DELETE dbo.OAIRecordDCType WHERE OAIRecordID = @OAIRecordID
	DELETE dbo.OAIRecordRight WHERE OAIRecordID = @OAIRecordID
	DELETE dbo.OAIRecordSubject WHERE OAIRecordID = @OAIRecordID
	DELETE dbo.OAIRecord WHERE OAIRecordID = @OAIRecordID

	COMMIT TRAN

END TRY
BEGIN CATCH

	IF (@@TRANCOUNT > 0) ROLLBACK TRAN
	THROW

END CATCH

END

GO
