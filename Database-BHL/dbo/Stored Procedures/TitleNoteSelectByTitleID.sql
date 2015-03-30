CREATE PROCEDURE [dbo].[TitleNoteSelectByTitleID]

@TitleID int

AS

BEGIN

SET NOCOUNT ON

SELECT	tn.TitleNoteID,
		tn.TitleID,
		tn.NoteTypeID,
		tn.NoteText,
		tn.NoteSequence,
		tn.CreationDate,
		tn.CreationUserID,
		nt.NoteTypeName,
		nt.NoteTypeDisplay,
		nt.MarcDataFieldTag,
		nt.MarcIndicator1
FROM	TitleNote tn 
		INNER JOIN NoteType nt ON tn.NoteTypeID = nt.NoteTypeID
WHERE	tn.TitleID = @TitleID
ORDER BY
		tn.NoteSequence, nt.MARCDataFieldTag, nt.MARCIndicator1

END
