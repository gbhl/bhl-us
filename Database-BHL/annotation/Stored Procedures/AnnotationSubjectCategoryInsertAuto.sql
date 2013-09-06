
-- AnnotationSubjectCategoryInsertAuto PROCEDURE
-- Generated 5/12/2010 3:45:46 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for AnnotationSubjectCategory

CREATE PROCEDURE annotation.AnnotationSubjectCategoryInsertAuto

@AnnotationSubjectCategoryID INT OUTPUT,
@AnnotationSourceID INT,
@SubjectCategoryCode NVARCHAR(20),
@SubjectCategoryName NVARCHAR(50)

AS 

SET NOCOUNT ON

INSERT INTO [annotation].[AnnotationSubjectCategory]
(
	[AnnotationSourceID],
	[SubjectCategoryCode],
	[SubjectCategoryName],
	[CreationDate],
	[LastModifiedDate]
)
VALUES
(
	@AnnotationSourceID,
	@SubjectCategoryCode,
	@SubjectCategoryName,
	getdate(),
	getdate()
)

SET @AnnotationSubjectCategoryID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationSubjectCategoryInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

