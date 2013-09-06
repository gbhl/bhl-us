
CREATE VIEW [annotation].[vwAnnotationConcept]
AS
SELECT	c1.AnnotationConceptCode, 
		c1.AnnotationSourceID, 
		ISNULL(c2.ConceptText + ': ', '') + c1.ConceptText AS ConceptText, 
		c1.ParentConceptCode, 
		c1.CreationDate, 
		c1.LastModifiedDate
FROM	annotation.AnnotationConcept c1 LEFT JOIN annotation.AnnotationConcept c2
			ON c1.ParentConceptCode = c2.AnnotationConceptCode
			AND c1.AnnotationSourceID = c2.AnnotationSourceID


