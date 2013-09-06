
-- AnnotationSubjectUpdateAuto PROCEDURE
-- Generated 5/11/2010 4:39:26 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for AnnotationSubject

CREATE PROCEDURE annotation.AnnotationSubjectUpdateAuto

@AnnotationSubjectID INT,
@AnnotationID INT,
@AnnotationSubjectCategoryID INT,
@AnnotationKeywordTargetID INT,
@SubjectText NVARCHAR(150)

AS 

SET NOCOUNT ON

UPDATE [annotation].[AnnotationSubject]

SET

	[AnnotationID] = @AnnotationID,
	[AnnotationSubjectCategoryID] = @AnnotationSubjectCategoryID,
	[AnnotationKeywordTargetID] = @AnnotationKeywordTargetID,
	[SubjectText] = @SubjectText,
	[LastModifiedDate] = getdate()

WHERE
	[AnnotationSubjectID] = @AnnotationSubjectID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationSubjectUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[AnnotationSubjectID],
		[AnnotationID],
		[AnnotationSubjectCategoryID],
		[AnnotationKeywordTargetID],
		[SubjectText],
		[CreationDate],
		[LastModifiedDate]

	FROM [annotation].[AnnotationSubject]
	
	WHERE
		[AnnotationSubjectID] = @AnnotationSubjectID
	
	RETURN -- update successful
END

