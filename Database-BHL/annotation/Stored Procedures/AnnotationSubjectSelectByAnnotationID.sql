
CREATE PROCEDURE [annotation].[AnnotationSubjectSelectByAnnotationID] 
	@AnnotationID int
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT	K.AnnotationKeywordTargetID, 
			K.KeywordTargetName,
			asub.[AnnotationSubjectID],
			asub.AnnotationSubjectCategoryID,
			asubcat.[SubjectCategoryCode],
			asubcat.[SubjectCategoryName],
			asub.[SubjectText]
	FROM	[annotation].[AnnotationSubject] asub INNER JOIN [annotation].AnnotationSubjectCategory asubcat
				ON asubcat.AnnotationSubjectCategoryID = asub.AnnotationSubjectCategoryID
			INNER JOIN AnnotationKeywordTarget K 
				ON K.AnnotationKeywordTargetID = asub.AnnotationKeywordTargetID
	WHERE	AnnotationID = @AnnotationID
	ORDER BY 
			K.KeywordTargetName, 
			asubcat.SubjectCategoryName, 
			asub.SubjectText
END

