-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [annotation].[AnnotatedPageTypeSelectByPageID]
	-- Add the parameters for the stored procedure here
	@PageID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT apt.* 
	FROM AnnotatedPage ap
	JOIN AnnotatedPageType apt
	ON apt.AnnotatedPageTypeID = ap.AnnotatedPageTypeID
	WHERE ap.PageID = @PageID
	
	
END
