
-- AnnotationConceptUpdateAuto PROCEDURE
-- Generated 1/7/2011 3:13:11 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for AnnotationConcept

CREATE PROCEDURE [annotation].AnnotationConceptUpdateAuto

@AnnotationConceptCode NVARCHAR(20),
@AnnotationSourceID INT,
@ConceptText NVARCHAR(100),
@ParentConceptCode NVARCHAR(20)

AS 

SET NOCOUNT ON

UPDATE [annotation].[AnnotationConcept]

SET

	[AnnotationConceptCode] = @AnnotationConceptCode,
	[AnnotationSourceID] = @AnnotationSourceID,
	[ConceptText] = @ConceptText,
	[ParentConceptCode] = @ParentConceptCode,
	[LastModifiedDate] = getdate()

WHERE
	[AnnotationConceptCode] = @AnnotationConceptCode
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationConceptUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[AnnotationConceptCode],
		[AnnotationSourceID],
		[ConceptText],
		[ParentConceptCode],
		[CreationDate],
		[LastModifiedDate]

	FROM [annotation].[AnnotationConcept]
	
	WHERE
		[AnnotationConceptCode] = @AnnotationConceptCode
	
	RETURN -- update successful
END

