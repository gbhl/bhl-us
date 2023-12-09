CREATE PROCEDURE annotation.AnnotationSubjectCategorySelect

@AnnotationSubjectCategoryID INT

AS 

SET NOCOUNT ON

SELECT	AnnotationSubjectCategoryID,
		AnnotationSourceID,
		SubjectCategoryCode,
		SubjectCategoryName,
		CreationDate,
		LastModifiedDate
FROM	annotation.AnnotationSubjectCategory
WHERE	AnnotationSubjectCategoryID = @AnnotationSubjectCategoryID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationSubjectCategorySelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
