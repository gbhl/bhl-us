
-- AnnotationPolygonInsertAuto PROCEDURE
-- Generated 6/25/2010 5:09:34 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for AnnotationPolygon

CREATE PROCEDURE [annotation].AnnotationPolygonInsertAuto

@AnnotationPolygonID INT OUTPUT,
@AnnotationID INT,
@PolygonX1 INT = null,
@PolygonY1 INT = null,
@PolygonX2 INT = null,
@PolygonY2 INT = null

AS 

SET NOCOUNT ON

INSERT INTO [annotation].[AnnotationPolygon]
(
	[AnnotationID],
	[PolygonX1],
	[PolygonY1],
	[PolygonX2],
	[PolygonY2],
	[CreationDate],
	[LastModifiedDate]
)
VALUES
(
	@AnnotationID,
	@PolygonX1,
	@PolygonY1,
	@PolygonX2,
	@PolygonY2,
	getdate(),
	getdate()
)

SET @AnnotationPolygonID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationPolygonInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

