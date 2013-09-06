
-- PageAnnotationSelectAuto PROCEDURE
-- Generated 5/11/2010 1:52:21 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for PageAnnotation

CREATE PROCEDURE annotation.PageAnnotationSelectAuto

@AnnotatedPageID INT,
@AnnotationID INT

AS 

SET NOCOUNT ON

SELECT 

	[AnnotatedPageID],
	[AnnotationID],
	[PageColumn]

FROM [annotation].[PageAnnotation]

WHERE
	[AnnotatedPageID] = @AnnotatedPageID AND
	[AnnotationID] = @AnnotationID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageAnnotationSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

