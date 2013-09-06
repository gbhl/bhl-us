
CREATE PROCEDURE [annotation].[AnnotationConceptSelectAll]

@AnnotationSourceID int

AS

BEGIN

SET NOCOUNT ON
		
SELECT DISTINCT
		v.AnnotationConceptCode,
		v.ConceptText,
		v.ParentConceptCode
FROM	annotation.vwAnnotationConcept v INNER JOIN annotation.Annotation_AnnotationConcept aac
			ON v.AnnotationConceptCode = aac.AnnotationConceptCode
WHERE	v.AnnotationSourceID = @AnnotationSourceID
ORDER BY 
		v.ConceptText
		
END 

