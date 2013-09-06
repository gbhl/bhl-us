
-- PageAnnotationUpdateAuto PROCEDURE
-- Generated 5/11/2010 1:52:21 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for PageAnnotation

CREATE PROCEDURE annotation.PageAnnotationUpdateAuto

@AnnotatedPageID INT,
@AnnotationID INT,
@PageColumn NVARCHAR(20)

AS 

SET NOCOUNT ON

UPDATE [annotation].[PageAnnotation]

SET

	[AnnotatedPageID] = @AnnotatedPageID,
	[AnnotationID] = @AnnotationID,
	[PageColumn] = @PageColumn

WHERE
	[AnnotatedPageID] = @AnnotatedPageID AND
	[AnnotationID] = @AnnotationID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageAnnotationUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

