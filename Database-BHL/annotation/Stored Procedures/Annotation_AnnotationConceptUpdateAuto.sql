
-- Annotation_AnnotationConceptUpdateAuto PROCEDURE
-- Generated 5/12/2010 1:57:10 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for Annotation_AnnotationConcept

CREATE PROCEDURE annotation.Annotation_AnnotationConceptUpdateAuto

@AnnotationID INT,
@AnnotationConceptCode NVARCHAR(20),
@AnnotationKeywordTargetID INT

AS 

SET NOCOUNT ON

UPDATE [annotation].[Annotation_AnnotationConcept]

SET

	[AnnotationID] = @AnnotationID,
	[AnnotationConceptCode] = @AnnotationConceptCode,
	[AnnotationKeywordTargetID] = @AnnotationKeywordTargetID,
	[LastModifiedDate] = getdate()

WHERE
	[AnnotationID] = @AnnotationID AND
	[AnnotationConceptCode] = @AnnotationConceptCode AND
	[AnnotationKeywordTargetID] = @AnnotationKeywordTargetID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure Annotation_AnnotationConceptUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	
	RETURN -- update successful
END

