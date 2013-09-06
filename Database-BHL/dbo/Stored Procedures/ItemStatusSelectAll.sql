create PROCEDURE [dbo].[ItemStatusSelectAll]
AS 

SET NOCOUNT ON

SELECT 
	[ItemStatusID],
	[ItemStatusName]
FROM [dbo].[ItemStatus]
order by itemstatusname