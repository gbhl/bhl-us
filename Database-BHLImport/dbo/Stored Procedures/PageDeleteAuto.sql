
-- PageDeleteAuto PROCEDURE
-- Generated 2/26/2008 3:15:49 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for Page

CREATE PROCEDURE PageDeleteAuto

@PageID INT

AS 

DELETE FROM [dbo].[Page]

WHERE

	[PageID] = @PageID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

