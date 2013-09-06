
CREATE PROCEDURE [annotation].[AnnotationSubjectInsertUnique]

@AnnotationID int,
@AnnotationSubjectCategoryID int,
@AnnotationKeywordTargetID int,
@SubjectText nvarchar(150)

AS
BEGIN
/*
 * Make sure the submitted data is unique before executing the INSERT
 */

IF NOT EXISTS (	SELECT	AnnotationSubjectID
				FROM	annotation.AnnotationSubject
				WHERE	AnnotationID = @AnnotationID
				AND		AnnotationSubjectCategoryID = @AnnotationSubjectCategoryID
				AND		AnnotationKeywordTargetID = @AnnotationKeywordTargetID
				AND		SubjectText = @SubjectText
				)
BEGIN
	INSERT INTO [annotation].[AnnotationSubject]
	(
		[AnnotationID],
		[AnnotationSubjectCategoryID],
		[AnnotationKeywordTargetID],
		[SubjectText],
		[CreationDate],
		[LastModifiedDate]
	)
	VALUES
	(
		@AnnotationID,
		@AnnotationSubjectCategoryID,
		@AnnotationKeywordTargetID,
		@SubjectText,
		getdate(),
		getdate()
	)
END

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationSubjectInsertUnique. No information was inserted as a result of this request.', 16, 1)
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
	
	WHERE	AnnotationID = @AnnotationID
	AND		AnnotationSubjectCategoryID = @AnnotationSubjectCategoryID
	AND		AnnotationKeywordTargetID = @AnnotationKeywordTargetID
	AND		SubjectText = @SubjectText
	
	RETURN -- insert successful
END

END


