
-- Annotation_AnnotationConceptInsertAuto PROCEDURE
-- Generated 5/12/2010 1:57:10 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for Annotation_AnnotationConcept

CREATE PROCEDURE annotation.Annotation_AnnotationConceptInsertAuto

@AnnotationID INT,
@AnnotationConceptCode NVARCHAR(20),
@AnnotationKeywordTargetID INT

AS 

SET NOCOUNT ON

INSERT INTO [annotation].[Annotation_AnnotationConcept]
(
	[AnnotationID],
	[AnnotationConceptCode],
	[AnnotationKeywordTargetID],
	[CreationDate],
	[LastModifiedDate]
)
VALUES
(
	@AnnotationID,
	@AnnotationConceptCode,
	@AnnotationKeywordTargetID,
	getdate(),
	getdate()
)

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure Annotation_AnnotationConceptInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

