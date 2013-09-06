-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [annotation].[AnnotationSelectByItemID]
	-- Add the parameters for the stored procedure here
	@ItemID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT annotatedPage.PageID, page.SequenceOrder, annotation.AnnotationID, annotation.AnnotationTextClean, annotation.AnnotationTextDisplay
	FROM [annotation].[AnnotatedItem] annotatedItem
	JOIN [annotation].AnnotatedPage annotatedPage
	ON annotatedPage.AnnotatedItemID = annotatedItem.AnnotatedItemID
	JOIN [annotation].PageAnnotation pageAnnotation
	ON pageAnnotation.AnnotatedPageID = annotatedPage.AnnotatedPageID
	JOIN [annotation].Annotation annotation
	ON annotation.AnnotationID = pageAnnotation.AnnotationID
	JOIN [dbo].Page page
	ON page.PageID = annotatedPage.PageID
	WHERE annotatedItem.ItemID = @ItemID
	ORDER BY page.SequenceOrder, pageAnnotation.AnnotationID
END
