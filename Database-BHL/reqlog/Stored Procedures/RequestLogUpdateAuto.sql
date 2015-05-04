CREATE PROCEDURE [reqlog].[RequestLogUpdateAuto]

@RequestLogID INT,
@ApplicationID INT,
@IPAddress VARCHAR(15),
@UserID INT,
@RequestTypeID INT,
@Detail VARCHAR(2000)

AS 

SET NOCOUNT ON

UPDATE	[reqlog].[RequestLog]
SET		[ApplicationID] = @ApplicationID,
		[IPAddress] = @IPAddress,
		[UserID] = @UserID,
		[RequestTypeID] = @RequestTypeID,
		[Detail] = @Detail
WHERE	[RequestLogID] = @RequestLogID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure RequestLogUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT	[RequestLogID],
			[ApplicationID],
			[IPAddress],
			[UserID],
			[CreationDate],
			[RequestTypeID],
			[Detail]
	FROM	[reqlog].[RequestLog]
	WHERE	[RequestLogID] = @RequestLogID
	
	RETURN -- update successful
END
