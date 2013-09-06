
-- AnnotationSubjectCategoryDeleteAuto PROCEDURE
-- Generated 5/12/2010 3:45:46 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for AnnotationSubjectCategory

CREATE PROCEDURE annotation.AnnotationSubjectCategoryDeleteAuto

@AnnotationSubjectCategoryID INT

AS 

DELETE FROM [annotation].[AnnotationSubjectCategory]

WHERE

	[AnnotationSubjectCategoryID] = @AnnotationSubjectCategoryID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationSubjectCategoryDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

