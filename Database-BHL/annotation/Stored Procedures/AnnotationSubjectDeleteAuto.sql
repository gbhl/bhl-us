
-- AnnotationSubjectDeleteAuto PROCEDURE
-- Generated 5/11/2010 4:39:26 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for AnnotationSubject

CREATE PROCEDURE annotation.AnnotationSubjectDeleteAuto

@AnnotationSubjectID INT

AS 

DELETE FROM [annotation].[AnnotationSubject]

WHERE

	[AnnotationSubjectID] = @AnnotationSubjectID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationSubjectDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

