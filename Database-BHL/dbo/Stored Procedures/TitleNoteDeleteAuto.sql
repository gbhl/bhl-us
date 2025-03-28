
-- TitleNoteDeleteAuto PROCEDURE
-- Generated 2/27/2015 2:20:32 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for TitleNote

CREATE PROCEDURE [dbo].[TitleNoteDeleteAuto]

@TitleNoteID INT

AS 

DELETE FROM [dbo].[TitleNote]

WHERE

	[TitleNoteID] = @TitleNoteID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleNoteDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END


GO
