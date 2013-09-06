
-- AnnotationMarkInsertAuto PROCEDURE
-- Generated 5/5/2010 3:04:42 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for AnnotationMark

CREATE PROCEDURE annotation.AnnotationMarkInsertAuto

@AnnotationMarkID INT OUTPUT,
@AnnotationID INT,
@ExternalIdentifier NVARCHAR(50),
@SequenceNumber INT,
@Position NVARCHAR(50),
@AnnotationMarkTypeID INT = null,
@Content NVARCHAR(MAX),
@CorrectedContent NVARCHAR(MAX),
@Comment NVARCHAR(MAX),
@PolygonX1 INT = null,
@PolygonY1 INT = null,
@PolygonX2 INT = null,
@PolygonY2 INT = null

AS 

SET NOCOUNT ON

INSERT INTO [annotation].[AnnotationMark]
(
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
)
VALUES
(
	@AnnotationID,
	@ExternalIdentifier,
	@SequenceNumber,
	@Position,
	@AnnotationMarkTypeID,
	@Content,
	@CorrectedContent,
	@Comment,
	@PolygonX1,
	@PolygonY1,
	@PolygonX2,
	@PolygonY2,
	getdate(),
	getdate()
)

SET @AnnotationMarkID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationMarkInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

