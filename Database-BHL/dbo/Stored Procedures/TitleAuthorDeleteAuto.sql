
-- TitleAuthorDeleteAuto PROCEDURE
-- Generated 5/29/2012 12:59:27 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for TitleAuthor

CREATE PROCEDURE TitleAuthorDeleteAuto

@TitleAuthorID INT

AS 

DELETE FROM [dbo].[TitleAuthor]

WHERE

	[TitleAuthorID] = @TitleAuthorID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleAuthorDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END


