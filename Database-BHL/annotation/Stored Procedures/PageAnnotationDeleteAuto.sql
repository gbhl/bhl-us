
-- PageAnnotationDeleteAuto PROCEDURE
-- Generated 5/11/2010 1:52:21 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for PageAnnotation

CREATE PROCEDURE annotation.PageAnnotationDeleteAuto

@AnnotatedPageID INT,
@AnnotationID INT

AS 

DELETE FROM [annotation].[PageAnnotation]

WHERE

	[AnnotatedPageID] = @AnnotatedPageID AND
	[AnnotationID] = @AnnotationID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageAnnotationDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

