CREATE PROCEDURE dbo.PageSelectCurrentUpdateHistory

@PageID int

AS

BEGIN

SET NOCOUNT ON

SELECT PageID, LastModifiedDate, LastModifiedUserID FROM dbo.Page WHERE	PageID = @PageID
UNION
SELECT PageID, LastModifiedDate, LastModifiedUserID FROM dbo.Page_PageType WHERE PageID = @PageID
UNION
SELECT PageID, LastModifiedDate, LastModifiedUserID FROM dbo.IndicatedPage WHERE PageID = @PageID

END
