
-- NameDeleteAuto PROCEDURE
-- Generated 12/10/2012 3:05:47 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for Name

CREATE PROCEDURE NameDeleteAuto

@NameID INT

AS 

DELETE FROM [dbo].[Name]

WHERE

	[NameID] = @NameID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure NameDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

