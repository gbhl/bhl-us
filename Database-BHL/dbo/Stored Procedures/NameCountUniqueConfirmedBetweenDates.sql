
CREATE PROCEDURE [dbo].[NameCountUniqueConfirmedBetweenDates]

@StartDate DateTime,
@EndDate DateTime

AS 

SET NOCOUNT ON

-- Count total names
SELECT	COUNT(DISTINCT ResolvedNameString)
FROM	dbo.NamePage np
		INNER JOIN dbo.Name n WITH (NOLOCK)
			ON np.NameID = n.NameID
		INNER JOIN dbo.NameResolved nr WITH (NOLOCK)
			ON n.NameResolvedID = nr.NameResolvedID
WHERE	n.IsActive = 1
AND		(np.CreationDate BETWEEN @StartDate AND @EndDate OR
		np.LastModifiedDate BETWEEN @StartDate AND @EndDate)

