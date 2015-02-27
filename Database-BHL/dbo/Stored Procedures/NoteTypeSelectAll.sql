CREATE PROCEDURE [dbo].[NoteTypeSelectAll]
AS 

SET NOCOUNT ON

SELECT	NoteTypeID,
		NoteTypeName,
		NoteTypeDisplay,
		MarcDataFieldTag,
		MarcIndicator1,
		CreationDate,
		LastModifiedDate,
		CreationUserID,
		LastModifiedUserID
FROM	dbo.NoteType
ORDER BY MarcDataFieldTag, MarcIndicator1, NoteTypeName
