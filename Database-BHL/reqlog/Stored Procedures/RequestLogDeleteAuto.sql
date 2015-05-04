CREATE PROCEDURE [reqlog].[RequestLogDeleteAuto]

@RequestLogID INT

AS 

DELETE FROM [reqlog].[RequestLog]

WHERE

	[RequestLogID] = @RequestLogID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure RequestLogDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END
