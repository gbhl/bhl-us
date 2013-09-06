
-- AnnotationPolygonSelectAuto PROCEDURE
-- Generated 6/25/2010 5:09:34 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for AnnotationPolygon

CREATE PROCEDURE [annotation].AnnotationPolygonSelectAuto

@AnnotationPolygonID INT

AS 

SET NOCOUNT ON

SELECT 

	[AnnotationPolygonID],
	[AnnotationID],
	[PolygonX1],
	[PolygonY1],
	[PolygonX2],
	[PolygonY2],
	[CreationDate],
	[LastModifiedDate]

FROM [annotation].[AnnotationPolygon]

WHERE
	[AnnotationPolygonID] = @AnnotationPolygonID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationPolygonSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

