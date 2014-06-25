CREATE PROCEDURE [dbo].[EntityCountSelectLatest]

AS

BEGIN

SET NOCOUNT ON

-- Select the most recent stats from the table
SELECT	c.EntityCountID, c.EntityCountTypeID, t.FullName, t.DisplayName, c.CountValue, c.CreationDate
FROM	dbo.EntityCount c 
		INNER JOIN (	SELECT	EntityCountTypeID, MAX(EntityCountID) AS EntityCountID
						FROM	dbo.EntityCount
						GROUP BY EntityCountTypeID	) x
			ON c.EntityCountTypeID = x.EntityCountTypeID
			AND c.EntityCountID = x.EntityCountID
		INNER JOIN dbo.EntityCountType t ON c.EntityCountTypeID = t.EntityCountTypeID
ORDER BY t.Sequence

END

