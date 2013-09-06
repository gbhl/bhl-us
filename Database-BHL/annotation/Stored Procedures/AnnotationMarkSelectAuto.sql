
-- AnnotationMarkSelectAuto PROCEDURE
-- Generated 5/5/2010 3:04:42 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for AnnotationMark

CREATE PROCEDURE annotation.AnnotationMarkSelectAuto

@AnnotationMarkID INT

AS 

SET NOCOUNT ON

SELECT 

	[AnnotationMarkID],
	[AnnotationID],
	[ExternalIdentifier],
	[SequenceNumber],
	[Position],
	[AnnotationMarkTypeID],
	[Content],
	[CorrectedContent],
	[Comment],
	[PolygonX1],
	[PolygonY1],
	[PolygonX2],
	[PolygonY2],
	[CreationDate],
	[LastModifiedDate]

FROM [annotation].[AnnotationMark]

WHERE
	[AnnotationMarkID] = @AnnotationMarkID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationMarkSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

