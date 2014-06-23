CREATE PROCEDURE [dbo].[Page_PageTypeInsert]

@PageID int,
@PageTypeName nvarchar(30),
@UserID int

AS

BEGIN

SET NOCOUNT ON

IF @PageTypeName = 'Painting/Drawing/Diagram' SET @PageTypeName = 'Drawing'
IF @PageTypeName = 'Chart/Table' SET @PageTypeName = 'Table'

-- Add the page type if it is not already there
IF NOT EXISTS (	SELECT	ppt.PageID 
				FROM	dbo.Page_PageType ppt INNER JOIN PageType pt ON ppt.PageTypeID = pt.PageTypeID
				WHERE	ppt.PageID = @PageID AND pt.PageTypeName = @PageTypeName)
BEGIN
	INSERT dbo.Page_PageType (PageID, PageTypeID, Verified, CreationUserID, LastModifiedUserID)
	SELECT	@PageID, PageTypeID, 1, @UserID, @UserID FROM dbo.PageType WHERE PageTypeName = @PageTypeName

	UPDATE dbo.Page SET LastModifiedDate = GETDATE(), LastModifiedUserID = @UserID WHERE PageID = @PageID
END

END

