SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [annotation].[AnnotationSelectByItemID]
	-- Add the parameters for the stored procedure here
	@ItemID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT	annotatedPage.PageID, 
			page.SequenceOrder, 
			annotation.AnnotationID, 
			annotation.AnnotationTextClean, 
			annotation.AnnotationTextDisplay
	FROM	[annotation].[AnnotatedItem] annotatedItem
			INNER JOIN [annotation].AnnotatedPage annotatedPage ON annotatedPage.AnnotatedItemID = annotatedItem.AnnotatedItemID
			INNER JOIN [annotation].PageAnnotation pageAnnotation ON pageAnnotation.AnnotatedPageID = annotatedPage.AnnotatedPageID
			INNER JOIN [annotation].Annotation annotation ON annotation.AnnotationID = pageAnnotation.AnnotationID
			INNER JOIN [dbo].ItemPage page ON page.PageID = annotatedPage.PageID AND page.ItemID = annotatedItem.ItemID
	WHERE	annotatedItem.ItemID = @ItemID
	ORDER BY 
			page.SequenceOrder, pageAnnotation.AnnotationID
END


GO
