CREATE PROCEDURE servlog.ServiceLogInsert
	@LogDate datetime,
	@ServiceName nvarchar(200),
	@ServiceParam nvarchar(30) = '',
	@SeverityName nvarchar(30) = 'Information',
	@ErrorNumber int = NULL,
	@Procedure nvarchar(500) = '', 
	@Line int = NULL,
	@Message nvarchar(max) = '',
	@StackTrace nvarchar(max) = ''
AS
BEGIN
	SET NOCOUNT ON 

	DECLARE @ServiceID int = NULL, @SeverityID int = NULL
	SELECT @ServiceID = ServiceID FROM servlog.[Service] WHERE [Name] = @ServiceName AND [Param] = @ServiceParam
	IF @ServiceID IS NULL
	BEGIN
		SELECT @ServiceID = ServiceID FROM servlog.[Service] WHERE [Name] = 'Unknown'
	END
	SELECT @SeverityID = SeverityID FROM servlog.Severity WHERE [Name] = @SeverityName
	IF @SeverityID IS NULL
	BEGIN
		SELECT @SeverityID = SeverityID FROM servlog.Severity WHERE [Name] = 'Information'
	END

	INSERT servlog.ServiceLog (ServiceID, SeverityID, ErrorNumber, [Procedure], Line, [Message], StackTrace, CreationDate)
	VALUES (@ServiceID, @SeverityID, @ErrorNumber, @Procedure, @Line, @Message, @StackTrace, @LogDate)
END
GO
