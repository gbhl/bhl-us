
-- PageNameDeleteAuto PROCEDURE
-- Generated 1/16/2008 1:54:48 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for PageName

CREATE PROCEDURE PageNameDeleteAuto

@PageNameID INT

AS 

DELETE FROM [dbo].[PageName]

WHERE

	[PageNameID] = @PageNameID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageNameDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

