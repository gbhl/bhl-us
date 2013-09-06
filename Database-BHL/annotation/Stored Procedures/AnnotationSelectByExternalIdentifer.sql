
CREATE PROCEDURE [annotation].[AnnotationSelectByExternalIdentifer]

@ExternalIdentifier nvarchar(50),
@AnnotationSourceID int

AS

BEGIN

SET NOCOUNT ON

SELECT	AnnotationID,
		AnnotationSourceID,
		ExternalIdentifier,
		SequenceNumber,
		AnnotationTextDescription,
		AnnotationText,
		AnnotationTextClean,
		AnnotationTextDisplay,
		AnnotationTextCorrected,
		Comment,
		CreationDate,
		LastModifiedDate
FROM	annotation.Annotation
WHERE	ExternalIdentifier = @ExternalIdentifier
AND		AnnotationSourceID = @AnnotationSourceID

END


