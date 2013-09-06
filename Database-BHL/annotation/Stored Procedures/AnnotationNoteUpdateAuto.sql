
-- AnnotationNoteUpdateAuto PROCEDURE
-- Generated 12/15/2010 3:05:49 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for AnnotationNote

CREATE PROCEDURE annotation.AnnotationNoteUpdateAuto

@AnnotationNoteID INT,
@AnnotationID INT,
@NoteText NVARCHAR(MAX),
@NoteTextClean NVARCHAR(MAX),
@NoteTextDisplay NVARCHAR(MAX),
@IsAlternate TINYINT

AS 

SET NOCOUNT ON

UPDATE annotation.[AnnotationNote]

SET

	[AnnotationID] = @AnnotationID,
	[NoteText] = @NoteText,
	[NoteTextClean] = @NoteTextClean,
	[NoteTextDisplay] = @NoteTextDisplay,
	[IsAlternate] = @IsAlternate,
	[LastModifiedDate] = getdate()

WHERE
	[AnnotationNoteID] = @AnnotationNoteID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationNoteUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

