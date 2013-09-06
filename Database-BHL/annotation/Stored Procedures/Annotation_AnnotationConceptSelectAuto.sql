
-- Annotation_AnnotationConceptSelectAuto PROCEDURE
-- Generated 5/12/2010 1:57:10 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for Annotation_AnnotationConcept

CREATE PROCEDURE annotation.Annotation_AnnotationConceptSelectAuto

@AnnotationID INT,
@AnnotationConceptCode NVARCHAR(20),
@AnnotationKeywordTargetID INT

AS 

SET NOCOUNT ON

SELECT 

	[AnnotationID],
	[AnnotationConceptCode],
	[AnnotationKeywordTargetID],
	[CreationDate],
	[LastModifiedDate]

FROM [annotation].[Annotation_AnnotationConcept]

WHERE
	[AnnotationID] = @AnnotationID AND
	[AnnotationConceptCode] = @AnnotationConceptCode AND
	[AnnotationKeywordTargetID] = @AnnotationKeywordTargetID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure Annotation_AnnotationConceptSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

