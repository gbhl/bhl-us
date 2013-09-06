
-- AnnotationPolygonUpdateAuto PROCEDURE
-- Generated 6/25/2010 5:09:34 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for AnnotationPolygon

CREATE PROCEDURE [annotation].AnnotationPolygonUpdateAuto

@AnnotationPolygonID INT,
@AnnotationID INT,
@PolygonX1 INT,
@PolygonY1 INT,
@PolygonX2 INT,
@PolygonY2 INT

AS 

SET NOCOUNT ON

UPDATE [annotation].[AnnotationPolygon]

SET

	[AnnotationID] = @AnnotationID,
	[PolygonX1] = @PolygonX1,
	[PolygonY1] = @PolygonY1,
	[PolygonX2] = @PolygonX2,
	[PolygonY2] = @PolygonY2,
	[LastModifiedDate] = getdate()

WHERE
	[AnnotationPolygonID] = @AnnotationPolygonID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationPolygonUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	
	RETURN -- update successful
END

