
-- AnnotationConceptDeleteAuto PROCEDURE
-- Generated 1/7/2011 3:13:11 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for AnnotationConcept

CREATE PROCEDURE [annotation].AnnotationConceptDeleteAuto

@AnnotationConceptCode NVARCHAR(20)

AS 

DELETE FROM [annotation].[AnnotationConcept]

WHERE

	[AnnotationConceptCode] = @AnnotationConceptCode

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationConceptDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

