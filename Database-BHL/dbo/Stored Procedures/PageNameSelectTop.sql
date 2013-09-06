CREATE PROCEDURE [dbo].[PageNameSelectTop] 
	@Number INT = 100
AS

SELECT TOP (@Number) NameConfirmed, Qty
FROM PageNameCount



