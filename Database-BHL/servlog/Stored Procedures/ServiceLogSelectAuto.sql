CREATE PROCEDURE servlog.ServiceLogSelectAuto

@ServiceLogID INT

AS 

SET NOCOUNT ON

SELECT	
	ServiceLogID,
	ServiceID,
	SeverityID,
	ErrorNumber,
	[Procedure],
	Line,
	[Message],
	StackTrace,
	CreationDate
FROM	
	servlog.ServiceLog
WHERE	
	ServiceLogID = @ServiceLogID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure servlog.ServiceLogSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
