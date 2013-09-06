create PROCEDURE [dbo].[ItemSelectPagination]
@ItemID int
AS 

SELECT 
	I.[ItemID],
	I.[BarCode],
	I.[ItemStatusID],
	I.[PaginationStatusID],
	I.PaginationStatusDate,
	I.PaginationStatusUserID,
	U.NameFirst + ' ' + U.NameLast AS PaginationUserName,
	PS.PaginationStatusName
FROM [dbo].[Item] I
	LEFT OUTER JOIN dbo.PaginationStatus PS ON PS.PaginationStatusID = I.PaginationStatusID
	LEFT OUTER JOIN dbo.MOBOTSecuritySecUserSyn U ON U.UserID = I.PaginationStatusUserID
WHERE
	I.ItemID = @ItemID