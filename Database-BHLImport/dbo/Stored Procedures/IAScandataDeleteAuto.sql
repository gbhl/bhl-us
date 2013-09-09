
-- IAScandataDeleteAuto PROCEDURE
-- Generated 11/24/2010 3:52:48 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for IAScandata

CREATE PROCEDURE IAScandataDeleteAuto

@ScandataID INT

AS 

DELETE FROM [dbo].[IAScandata]

WHERE

	[ScandataID] = @ScandataID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAScandataDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

