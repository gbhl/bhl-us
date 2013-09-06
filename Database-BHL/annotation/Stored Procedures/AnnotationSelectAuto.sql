
-- AnnotationSelectAuto PROCEDURE
-- Generated 12/15/2010 3:05:49 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for Annotation

CREATE PROCEDURE annotation.AnnotationSelectAuto

@AnnotationID INT

AS 

SET NOCOUNT ON

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

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

