
-- AnnotationDeleteAuto PROCEDURE
-- Generated 12/15/2010 3:05:49 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for Annotation

CREATE PROCEDURE annotation.AnnotationDeleteAuto

@AnnotationID INT

AS 

DELETE FROM annotation.[Annotation]

WHERE

	[AnnotationID] = @AnnotationID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

