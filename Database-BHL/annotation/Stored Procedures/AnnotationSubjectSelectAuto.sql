
-- AnnotationSubjectSelectAuto PROCEDURE
-- Generated 5/11/2010 4:39:26 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for AnnotationSubject

CREATE PROCEDURE annotation.AnnotationSubjectSelectAuto

@AnnotationSubjectID INT

AS 

SET NOCOUNT ON

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

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationSubjectSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

