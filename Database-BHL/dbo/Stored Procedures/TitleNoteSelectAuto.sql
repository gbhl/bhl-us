
-- TitleNoteSelectAuto PROCEDURE
-- Generated 2/27/2015 2:20:32 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for TitleNote

CREATE PROCEDURE [dbo].[TitleNoteSelectAuto]

@TitleNoteID INT

AS 

SET NOCOUNT ON

SELECT 

	[TitleNoteID],
	[TitleID],
	[NoteTypeID],
	[NoteText],
	[NoteSequence],
	[CreationDate],
	[CreationUserID]

FROM [dbo].[TitleNote]

WHERE
	[TitleNoteID] = @TitleNoteID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleNoteSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


GO
