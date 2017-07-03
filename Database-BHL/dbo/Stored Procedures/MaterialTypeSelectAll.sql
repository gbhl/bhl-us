CREATE PROCEDURE [dbo].[MaterialTypeSelectAll]
AS 

SET NOCOUNT ON

SELECT	[MaterialTypeID],
		[MaterialTypeName],
		[MaterialTypeLabel],
		[MARCCode]
FROM	[dbo].[MaterialType]
ORDER BY 
		[MaterialTypeLabel]
