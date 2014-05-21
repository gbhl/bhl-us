CREATE PROCEDURE dbo.ItemSelectByPageID

@PageID int

AS 

SET NOCOUNT ON

BEGIN

SELECT	i.ItemID,
		i.Barcode,
		i.Volume,
		i.Year,
		i.PaginationStatusID,
		i.PaginationStatusUserID,
		i.CreationDate,
		i.CreationUserID,
		i.LastModifiedDate,
		i.LastModifiedUserID
FROM	dbo.Item i
		INNER JOIN dbo.Page p ON i.ItemID = p.ItemID
WHERE	p.PageID = @PageID

END 