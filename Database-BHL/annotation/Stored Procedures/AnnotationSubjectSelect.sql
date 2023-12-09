CREATE PROCEDURE annotation.AnnotationSubjectSelect

@AnnotationSubjectID INT

AS 

SET NOCOUNT ON

SELECT	AnnotationSubjectID,
		AnnotationID,
		AnnotationSubjectCategoryID,
		AnnotationKeywordTargetID,
		SubjectText,
		CreationDate,
		LastModifiedDate
FROM	annotation.AnnotationSubject
WHERE	AnnotationSubjectID = @AnnotationSubjectID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationSubjectSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
