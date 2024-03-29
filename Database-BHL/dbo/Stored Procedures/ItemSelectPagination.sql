SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ItemSelectPagination]
@ItemID int
AS 

SELECT 
	b.BookID AS ItemID,
	b.[BarCode],
	I.[ItemStatusID],
	b.[PaginationStatusID],
	b.PaginationStatusDate,
	b.PaginationStatusUserID,
	U.FirstName + ' ' + U.LastName AS PaginationUserName,
	PS.PaginationStatusName
FROM [dbo].[Item] I
	INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
	LEFT OUTER JOIN dbo.PaginationStatus PS ON PS.PaginationStatusID = b.PaginationStatusID
	LEFT OUTER JOIN dbo.AspNetUsers U ON U.Id = b.PaginationStatusUserID
WHERE
	b.BookID = @ItemID


GO
