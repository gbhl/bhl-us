
-- AnnotationConceptSelectAuto PROCEDURE
-- Generated 1/7/2011 3:13:11 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for AnnotationConcept

CREATE PROCEDURE [annotation].AnnotationConceptSelectAuto

@AnnotationConceptCode NVARCHAR(20)

AS 

SET NOCOUNT ON

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

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationConceptSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

