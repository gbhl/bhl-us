
-- AuthorRoleDeleteAuto PROCEDURE
-- Generated 5/18/2012 11:11:49 AM
-- Do not modify the contents of this procedure.
-- Delete Procedure for AuthorRole

CREATE PROCEDURE AuthorRoleDeleteAuto

@AuthorRoleID INT

AS 

DELETE FROM [dbo].[AuthorRole]

WHERE

	[AuthorRoleID] = @AuthorRoleID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AuthorRoleDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END


