
-- AnnotationSubjectCategorySelectAuto PROCEDURE
-- Generated 5/12/2010 3:45:46 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for AnnotationSubjectCategory

CREATE PROCEDURE annotation.AnnotationSubjectCategorySelectAuto

@AnnotationSubjectCategoryID INT

AS 

SET NOCOUNT ON

SELECT 

	[AnnotationSubjectCategoryID],
	[AnnotationSourceID],
	[SubjectCategoryCode],
	[SubjectCategoryName],
	[CreationDate],
	[LastModifiedDate]

FROM [annotation].[AnnotationSubjectCategory]

WHERE
	[AnnotationSubjectCategoryID] = @AnnotationSubjectCategoryID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationSubjectCategorySelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

