CREATE PROCEDURE annotation.AnnotationConceptSelectByCode

@AnnotationConceptCode nvarchar(20)

AS

BEGIN

SET NOCOUNT ON

SELECT	AnnotationConceptCode,
		AnnotationSourceID,
		ConceptText,
		ParentConceptCode
FROM	annotation.vwAnnotationConcept
WHERE	AnnotationConceptCode = @AnnotationConceptCode

END

