
-- TitleDeleteAuto PROCEDURE
-- Generated 8/3/2010 11:16:34 AM
-- Do not modify the contents of this procedure.
-- Delete Procedure for Title

CREATE PROCEDURE TitleDeleteAuto

@TitleID INT

AS 

DELETE FROM [dbo].[Title]

WHERE

	[TitleID] = @TitleID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

