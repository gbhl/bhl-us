
-- AnnotationMarkDeleteAuto PROCEDURE
-- Generated 5/5/2010 3:04:42 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for AnnotationMark

CREATE PROCEDURE annotation.AnnotationMarkDeleteAuto

@AnnotationMarkID INT

AS 

DELETE FROM [annotation].[AnnotationMark]

WHERE

	[AnnotationMarkID] = @AnnotationMarkID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationMarkDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

