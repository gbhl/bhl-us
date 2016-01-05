CREATE PROCEDURE [dbo].[ItemSelectPagination]
@ItemID int
AS 

SELECT 
	I.[ItemID],
	I.[BarCode],
	I.[ItemStatusID],
	I.[PaginationStatusID],
	I.PaginationStatusDate,
	I.PaginationStatusUserID,
	U.FirstName + ' ' + U.LastName AS PaginationUserName,
	PS.PaginationStatusName
FROM [dbo].[Item] I
	LEFT OUTER JOIN dbo.PaginationStatus PS ON PS.PaginationStatusID = I.PaginationStatusID
	LEFT OUTER JOIN dbo.AspNetUsers U ON U.Id = I.PaginationStatusUserID
WHERE
	I.ItemID = @ItemID
