CREATE PROCEDURE [reqlog].[RequestLogSelectAuto]

@RequestLogID INT

AS 

SET NOCOUNT ON

SELECT	[RequestLogID],
		[ApplicationID],
		[IPAddress],
		[UserID],
		[CreationDate],
		[RequestTypeID],
		[Detail]
FROM	[reqlog].[RequestLog]
WHERE	[RequestLogID] = @RequestLogID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure RequestLogSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
