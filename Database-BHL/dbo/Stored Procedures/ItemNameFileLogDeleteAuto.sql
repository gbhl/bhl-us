
-- ItemNameFileLogDeleteAuto PROCEDURE
-- Generated 11/19/2009 2:21:40 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for ItemNameFileLog

CREATE PROCEDURE ItemNameFileLogDeleteAuto

@LogID INT

AS 

DELETE FROM [dbo].[ItemNameFileLog]

WHERE

	[LogID] = @LogID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemNameFileLogDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

