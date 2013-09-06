
-- AnnotationUpdateAuto PROCEDURE
-- Generated 12/15/2010 3:05:49 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for Annotation

CREATE PROCEDURE annotation.AnnotationUpdateAuto

@AnnotationID INT,
@AnnotationSourceID INT,
@ExternalIdentifier NVARCHAR(50),
@SequenceNumber INT,
@AnnotationTextDescription NVARCHAR(MAX),
@AnnotationText NVARCHAR(MAX),
@AnnotationTextClean NVARCHAR(MAX),
@AnnotationTextDisplay NVARCHAR(MAX),
@AnnotationTextCorrected NVARCHAR(MAX),
@Comment NVARCHAR(MAX)

AS 

SET NOCOUNT ON

UPDATE annotation.[Annotation]

SET

	[AnnotationSourceID] = @AnnotationSourceID,
	[ExternalIdentifier] = @ExternalIdentifier,
	[SequenceNumber] = @SequenceNumber,
	[AnnotationTextDescription] = @AnnotationTextDescription,
	[AnnotationText] = @AnnotationText,
	[AnnotationTextClean] = @AnnotationTextClean,
	[AnnotationTextDisplay] = @AnnotationTextDisplay,
	[AnnotationTextCorrected] = @AnnotationTextCorrected,
	[Comment] = @Comment,
	[LastModifiedDate] = getdate()

WHERE
	[AnnotationID] = @AnnotationID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[AnnotationID],
		[AnnotationSourceID],
		[ExternalIdentifier],
		[SequenceNumber],
		[AnnotationTextDescription],
		[AnnotationText],
		[AnnotationTextClean],
		[AnnotationTextDisplay],
		[AnnotationTextCorrected],
		[Comment],
		[CreationDate],
		[LastModifiedDate]

	FROM annotation.[Annotation]
	
	WHERE
		[AnnotationID] = @AnnotationID
	
	RETURN -- update successful
END

