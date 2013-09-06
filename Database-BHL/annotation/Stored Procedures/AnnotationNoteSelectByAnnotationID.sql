-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [annotation].[AnnotationNoteSelectByAnnotationID]
@AnnotationID INT

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
	[AnnotationID] = @AnnotationID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationNoteSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
