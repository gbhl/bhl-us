
-- AnnotationSubjectCategoryUpdateAuto PROCEDURE
-- Generated 5/12/2010 3:45:46 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for AnnotationSubjectCategory

CREATE PROCEDURE annotation.AnnotationSubjectCategoryUpdateAuto

@AnnotationSubjectCategoryID INT,
@AnnotationSourceID INT,
@SubjectCategoryCode NVARCHAR(20),
@SubjectCategoryName NVARCHAR(50)

AS 

SET NOCOUNT ON

UPDATE [annotation].[AnnotationSubjectCategory]

SET

	[AnnotationSourceID] = @AnnotationSourceID,
	[SubjectCategoryCode] = @SubjectCategoryCode,
	[SubjectCategoryName] = @SubjectCategoryName,
	[LastModifiedDate] = getdate()

WHERE
	[AnnotationSubjectCategoryID] = @AnnotationSubjectCategoryID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationSubjectCategoryUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	
	RETURN -- update successful
END

