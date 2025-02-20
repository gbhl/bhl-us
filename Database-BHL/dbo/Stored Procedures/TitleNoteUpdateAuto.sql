
-- TitleNoteUpdateAuto PROCEDURE
-- Generated 2/27/2015 2:20:32 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for TitleNote

CREATE PROCEDURE [dbo].[TitleNoteUpdateAuto]

@TitleNoteID INT,
@TitleID INT,
@NoteTypeID INT,
@NoteText NVARCHAR(MAX),
@NoteSequence SMALLINT

AS 

SET NOCOUNT ON

UPDATE [dbo].[TitleNote]

SET

	[TitleID] = @TitleID,
	[NoteTypeID] = @NoteTypeID,
	[NoteText] = @NoteText,
	[NoteSequence] = @NoteSequence

WHERE
	[TitleNoteID] = @TitleNoteID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleNoteUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	
	RETURN -- update successful
END


GO
