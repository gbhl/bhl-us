
CREATE PROCEDURE [annotation].[AnnotationSubjectSelectBySubjectText]

@SubjectText nvarchar(150),
@AnnotationSourceID int

AS

BEGIN

SET NOCOUNT ON
		
SELECT DISTINCT
		MIN(s.AnnotationSubjectID) AS AnnotationSubjectID,
		s.AnnotationSubjectCategoryID,
		c.SubjectCategoryName,
		s.SubjectText
FROM	annotation.AnnotationSubject s INNER JOIN annotation.PageAnnotation pa
			ON s.AnnotationID = pa.AnnotationID
		INNER JOIN annotation.AnnotationSubjectCategory c
			ON s.AnnotationSubjectCategoryID = c.AnnotationSubjectCategoryID
		INNER JOIN annotation.AnnotatedPage ap  -- Need to make sure the annotation is attached to a scanned page
			ON pa.AnnotatedPageID = ap.AnnotatedPageID
WHERE	c.AnnotationSourceID = @AnnotationSourceID
AND		s.SubjectText LIKE '%' + @SubjectText + '%'
AND		ap.PageID IS NOT NULL 
GROUP BY
		s.AnnotationSubjectCategoryID,
		c.SubjectCategoryName,
		s.SubjectText
ORDER BY 
		c.SubjectCategoryName,
		s.SubjectText
		
END 



