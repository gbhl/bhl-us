
-- AnnotationMarkUpdateAuto PROCEDURE
-- Generated 5/5/2010 3:04:42 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for AnnotationMark

CREATE PROCEDURE annotation.AnnotationMarkUpdateAuto

@AnnotationMarkID INT,
@AnnotationID INT,
@ExternalIdentifier NVARCHAR(50),
@SequenceNumber INT,
@Position NVARCHAR(50),
@AnnotationMarkTypeID INT,
@Content NVARCHAR(MAX),
@CorrectedContent NVARCHAR(MAX),
@Comment NVARCHAR(MAX),
@PolygonX1 INT,
@PolygonY1 INT,
@PolygonX2 INT,
@PolygonY2 INT

AS 

SET NOCOUNT ON

UPDATE [annotation].[AnnotationMark]

SET

	[AnnotationID] = @AnnotationID,
	[ExternalIdentifier] = @ExternalIdentifier,
	[SequenceNumber] = @SequenceNumber,
	[Position] = @Position,
	[AnnotationMarkTypeID] = @AnnotationMarkTypeID,
	[Content] = @Content,
	[CorrectedContent] = @CorrectedContent,
	[Comment] = @Comment,
	[PolygonX1] = @PolygonX1,
	[PolygonY1] = @PolygonY1,
	[PolygonX2] = @PolygonX2,
	[PolygonY2] = @PolygonY2,
	[LastModifiedDate] = getdate()

WHERE
	[AnnotationMarkID] = @AnnotationMarkID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationMarkUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	
	RETURN -- update successful
END

