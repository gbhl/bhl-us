
-- PageAnnotationInsertAuto PROCEDURE
-- Generated 5/11/2010 1:52:21 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for PageAnnotation

CREATE PROCEDURE annotation.PageAnnotationInsertAuto

@AnnotatedPageID INT,
@AnnotationID INT,
@PageColumn NVARCHAR(20)

AS 

SET NOCOUNT ON

INSERT INTO [annotation].[PageAnnotation]
(
	[AnnotatedPageID],
	[AnnotationID],
	[PageColumn]
)
VALUES
(
	@AnnotatedPageID,
	@AnnotationID,
	@PageColumn
)

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageAnnotationInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[AnnotatedPageID],
		[AnnotationID],
		[PageColumn]	

	FROM [annotation].[PageAnnotation]
	
	WHERE
		[AnnotatedPageID] = @AnnotatedPageID AND
		[AnnotationID] = @AnnotationID
	
	RETURN -- insert successful
END

