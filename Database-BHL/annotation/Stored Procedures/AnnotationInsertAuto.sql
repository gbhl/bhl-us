
-- AnnotationInsertAuto PROCEDURE
-- Generated 12/15/2010 3:05:49 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for Annotation

CREATE PROCEDURE annotation.AnnotationInsertAuto

@AnnotationID INT OUTPUT,
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

INSERT INTO annotation.[Annotation]
(
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
)
VALUES
(
	@AnnotationSourceID,
	@ExternalIdentifier,
	@SequenceNumber,
	@AnnotationTextDescription,
	@AnnotationText,
	@AnnotationTextClean,
	@AnnotationTextDisplay,
	@AnnotationTextCorrected,
	@Comment,
	getdate(),
	getdate()
)

SET @AnnotationID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

