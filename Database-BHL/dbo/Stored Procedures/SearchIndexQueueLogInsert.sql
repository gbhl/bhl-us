CREATE PROCEDURE dbo.SearchIndexQueueLogInsert

@LastAuditBasicID int,
@LastAuditDate datetime,
@NumberQueued int

AS

BEGIN

SET NOCOUNT ON

INSERT INTO [dbo].[SearchIndexQueueLog] ( [LastAuditBasicID], [LastAuditDate], [NumberQueued] )
VALUES ( @LastAuditBasicID, @LastAuditDate, @NumberQueued )

IF @@ERROR <> 0
BEGIN
	RAISERROR('An error occurred in procedure SearchIndexQueueLogInsert. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error
END
ELSE BEGIN
	RETURN -- success
END

END

