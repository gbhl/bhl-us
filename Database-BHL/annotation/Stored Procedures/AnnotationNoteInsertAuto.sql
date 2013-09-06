
-- AnnotationNoteInsertAuto PROCEDURE
-- Generated 12/15/2010 3:05:49 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for AnnotationNote

CREATE PROCEDURE annotation.AnnotationNoteInsertAuto

@AnnotationNoteID INT OUTPUT,
@AnnotationID INT,
@NoteText NVARCHAR(MAX),
@NoteTextClean NVARCHAR(MAX),
@NoteTextDisplay NVARCHAR(MAX),
@IsAlternate TINYINT

AS 

SET NOCOUNT ON

INSERT INTO annotation.[AnnotationNote]
(
	[AnnotationID],
	[NoteText],
	[NoteTextClean],
	[NoteTextDisplay],
	[IsAlternate],
	[CreationDate],
	[LastModifiedDate]
)
VALUES
(
	@AnnotationID,
	@NoteText,
	@NoteTextClean,
	@NoteTextDisplay,
	@IsAlternate,
	getdate(),
	getdate()
)

SET @AnnotationNoteID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationNoteInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[AnnotationNoteID],
		[AnnotationID],
		[NoteText],
		[NoteTextClean],
		[NoteTextDisplay],
		[IsAlternate],
		[CreationDate],
		[LastModifiedDate]	

	FROM annotation.[AnnotationNote]
	
	WHERE
		[AnnotationNoteID] = @AnnotationNoteID
	
	RETURN -- insert successful
END

