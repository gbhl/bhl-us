CREATE PROCEDURE [annotation].[AnnotationSubjectCategorySelectByCode]

@SubjectCategoryCode nvarchar(20),
@AnnotationSourceID int

AS

BEGIN

SELECT	AnnotationSubjectCategoryID,
		AnnotationSourceID,
		SubjectCategoryCode,
		SubjectCategoryName,
		CreationDate,
		LastModifiedDate
FROM	annotation.AnnotationSubjectCategory
WHERE	SubjectCategoryCode = @SubjectCategoryCode
AND		AnnotationSourceID = @AnnotationSourceID

END



