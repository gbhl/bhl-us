
-- AnnotationSubjectInsertAuto PROCEDURE
-- Generated 5/11/2010 4:39:26 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for AnnotationSubject

CREATE PROCEDURE annotation.AnnotationSubjectInsertAuto

@AnnotationSubjectID INT OUTPUT,
@AnnotationID INT,
@AnnotationSubjectCategoryID INT = null,
@AnnotationKeywordTargetID INT,
@SubjectText NVARCHAR(150)

AS 

SET NOCOUNT ON

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

SET @AnnotationSubjectID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationSubjectInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

