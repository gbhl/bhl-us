
-- AnnotationPolygonDeleteAuto PROCEDURE
-- Generated 6/25/2010 5:09:34 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for AnnotationPolygon

CREATE PROCEDURE [annotation].AnnotationPolygonDeleteAuto

@AnnotationPolygonID INT

AS 

DELETE FROM [annotation].[AnnotationPolygon]

WHERE

	[AnnotationPolygonID] = @AnnotationPolygonID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationPolygonDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

