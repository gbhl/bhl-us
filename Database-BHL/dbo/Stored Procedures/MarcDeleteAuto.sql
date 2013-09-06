
-- MarcDeleteAuto PROCEDURE
-- Generated 4/21/2009 3:39:50 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for Marc

CREATE PROCEDURE MarcDeleteAuto

@MarcID INT

AS 

DELETE FROM [dbo].[Marc]

WHERE

	[MarcID] = @MarcID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure MarcDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

