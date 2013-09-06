
-- AnnotationNoteSelectAuto PROCEDURE
-- Generated 12/15/2010 3:05:49 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for AnnotationNote

CREATE PROCEDURE annotation.AnnotationNoteSelectAuto

@AnnotationNoteID INT

AS 

SET NOCOUNT ON

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

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationNoteSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

