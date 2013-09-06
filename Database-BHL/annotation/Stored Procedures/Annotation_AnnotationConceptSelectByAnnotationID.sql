
CREATE PROCEDURE [annotation].[Annotation_AnnotationConceptSelectByAnnotationID]
	@AnnotationID int
AS
BEGIN

	SET NOCOUNT ON
	
	SELECT	K.AnnotationKeywordTargetID, K.KeywordTargetName, c.AnnotationConceptCode, C.ConceptText 	
	FROM	annotation.Annotation_AnnotationConcept A INNER JOIN annotation.vwAnnotationConcept C
				ON C.AnnotationConceptCode = A.AnnotationConceptCode
			INNER JOIN AnnotationKeywordTarget K
				ON K.AnnotationKeywordTargetID = A.AnnotationKeywordTargetID
	WHERE	A.AnnotationID = @AnnotationID
	ORDER BY 
			K.KeywordTargetName, C.ConceptText 

END

