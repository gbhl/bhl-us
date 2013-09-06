
CREATE PROCEDURE [dbo].[NameCountUniqueConfirmed]

AS 

SET NOCOUNT ON

-- Count total names
SELECT	COUNT(DISTINCT ResolvedNameString)
FROM	dbo.Name n WITH (NOLOCK)
		INNER JOIN dbo.NameResolved nr WITH (NOLOCK)
			ON n.NameResolvedID = nr.NameResolvedID
WHERE	n.IsActive = 1

