CREATE PROCEDURE [reqlog].[RequestLogInsertAuto]

@RequestLogID INT OUTPUT,
@ApplicationID INT,
@IPAddress VARCHAR(15) = null,
@UserID INT = null,
@RequestTypeID INT,
@Detail VARCHAR(2000) = null

AS 

SET NOCOUNT ON

INSERT INTO [reqlog].[RequestLog]
(
	[ApplicationID],
	[IPAddress],
	[UserID],
	[CreationDate],
	[RequestTypeID],
	[Detail]
)
VALUES
(
	@ApplicationID,
	@IPAddress,
	@UserID,
	getdate(),
	@RequestTypeID,
	@Detail
)

SET @RequestLogID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure RequestLogInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[RequestLogID],
		[ApplicationID],
		[IPAddress],
		[UserID],
		[CreationDate],
		[RequestTypeID],
		[Detail]	

	FROM [reqlog].[RequestLog]
	
	WHERE
		[RequestLogID] = @RequestLogID
	
	RETURN -- insert successful
END
