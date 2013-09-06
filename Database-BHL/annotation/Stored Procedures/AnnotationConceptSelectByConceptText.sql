
CREATE PROCEDURE [annotation].[AnnotationConceptSelectByConceptText]

@ConceptText nvarchar(100),
@AnnotationSourceID int

AS

BEGIN

SET NOCOUNT ON
		
SELECT DISTINCT
		ac.AnnotationConceptCode,
		ac.ConceptText,
		ac.ParentConceptCode
FROM	annotation.vwAnnotationConcept ac INNER JOIN annotation.Annotation_AnnotationConcept aac
			ON ac.AnnotationConceptCode = aac.AnnotationConceptCode
		INNER JOIN annotation.PageAnnotation pa  -- Need to make sure the annotation concepts are attached to an annotation
			ON aac.AnnotationID = pa.AnnotationID
		INNER JOIN annotation.AnnotatedPage ap  -- Need to make sure the annotation is attached to a scanned page
			ON pa.AnnotatedPageID = ap.AnnotatedPageID
WHERE	ac.AnnotationSourceID = @AnnotationSourceID
AND		ac.ConceptText LIKE '%' + @ConceptText + '%'
AND		ap.PageID IS NOT NULL 
ORDER BY 
		ac.ConceptText
		
END 


