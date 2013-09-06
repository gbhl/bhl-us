
-- AnnotationConceptInsertAuto PROCEDURE
-- Generated 1/7/2011 3:13:11 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for AnnotationConcept

CREATE PROCEDURE [annotation].AnnotationConceptInsertAuto

@AnnotationConceptCode NVARCHAR(20),
@AnnotationSourceID INT,
@ConceptText NVARCHAR(100),
@ParentConceptCode NVARCHAR(20) = null

AS 

SET NOCOUNT ON

INSERT INTO [annotation].[AnnotationConcept]
(
	[AnnotationConceptCode],
	[AnnotationSourceID],
	[ConceptText],
	[ParentConceptCode],
	[CreationDate],
	[LastModifiedDate]
)
VALUES
(
	@AnnotationConceptCode,
	@AnnotationSourceID,
	@ConceptText,
	@ParentConceptCode,
	getdate(),
	getdate()
)

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationConceptInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

