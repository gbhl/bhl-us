
-- Title_CreatorDeleteAuto PROCEDURE
-- Generated 9/4/2008 2:16:32 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for Title_Creator

CREATE PROCEDURE Title_CreatorDeleteAuto

@TitleCreatorID INT

AS 

DELETE FROM [dbo].[Title_Creator]

WHERE

	[TitleCreatorID] = @TitleCreatorID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure Title_CreatorDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

