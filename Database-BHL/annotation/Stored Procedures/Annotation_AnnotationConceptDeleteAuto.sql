
-- Annotation_AnnotationConceptDeleteAuto PROCEDURE
-- Generated 5/12/2010 1:57:10 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for Annotation_AnnotationConcept

CREATE PROCEDURE annotation.Annotation_AnnotationConceptDeleteAuto

@AnnotationID INT,
@AnnotationConceptCode NVARCHAR(20),
@AnnotationKeywordTargetID INT

AS 

DELETE FROM [annotation].[Annotation_AnnotationConcept]

WHERE

	[AnnotationID] = @AnnotationID AND
	[AnnotationConceptCode] = @AnnotationConceptCode AND
	[AnnotationKeywordTargetID] = @AnnotationKeywordTargetID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure Annotation_AnnotationConceptDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

