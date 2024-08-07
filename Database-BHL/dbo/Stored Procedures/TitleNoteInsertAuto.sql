
-- TitleNoteInsertAuto PROCEDURE
-- Generated 2/27/2015 2:20:32 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for TitleNote

CREATE PROCEDURE [dbo].[TitleNoteInsertAuto]

@TitleNoteID INT OUTPUT,
@TitleID INT,
@NoteTypeID INT = null,
@NoteText NVARCHAR(MAX),
@NoteSequence SMALLINT = null,
@CreationUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[TitleNote]
(
	[TitleID],
	[NoteTypeID],
	[NoteText],
	[NoteSequence],
	[CreationDate],
	[CreationUserID]
)
VALUES
(
	@TitleID,
	@NoteTypeID,
	@NoteText,
	@NoteSequence,
	getdate(),
	@CreationUserID
)

SET @TitleNoteID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleNoteInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END


GO
