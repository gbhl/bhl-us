
-- AuthorNameDeleteAuto PROCEDURE
-- Generated 5/29/2012 12:59:27 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for AuthorName

CREATE PROCEDURE AuthorNameDeleteAuto

@AuthorNameID INT

AS 

DELETE FROM [dbo].[AuthorName]

WHERE

	[AuthorNameID] = @AuthorNameID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AuthorNameDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END


