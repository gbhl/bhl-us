
-- ScanningRequestDeleteAuto PROCEDURE
-- Generated 6/10/2010 3:21:58 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for ScanningRequest

CREATE PROCEDURE ScanningRequestDeleteAuto

@ScanningRequestID INT

AS 

DELETE FROM [dbo].[ScanningRequest]

WHERE

	[ScanningRequestID] = @ScanningRequestID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ScanningRequestDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

