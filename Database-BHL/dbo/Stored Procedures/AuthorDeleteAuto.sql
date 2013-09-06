
-- AuthorDeleteAuto PROCEDURE
-- Generated 5/29/2012 12:59:27 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for Author

CREATE PROCEDURE AuthorDeleteAuto

@AuthorID INT

AS 

DELETE FROM [dbo].[Author]

WHERE

	[AuthorID] = @AuthorID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AuthorDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END


