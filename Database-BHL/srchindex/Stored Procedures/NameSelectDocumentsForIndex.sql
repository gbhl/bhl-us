﻿CREATE PROCEDURE [srchindex].[NameSelectDocumentsForIndex]

@StartID int,
@EndID int = NULL

AS 

BEGIN

SET NOCOUNT ON

SELECT TOP 200000
		r.NameResolvedID,
		r.ResolvedNameString,
		SUM(CASE WHEN i.ItemStatusID = 40 AND p.Active = 1 AND n.IsActive = 1 THEN 1 ELSE 0 END) AS NameCount
FROM	dbo.NameResolved r WITH (NOLOCK)
		INNER JOIN dbo.Name n WITH (NOLOCK) ON r.NameResolvedID = n.NameResolvedID
		LEFT JOIN dbo.NamePage np WITH (NOLOCK) ON n.NameID = np.NameID
		LEFT JOIN dbo.Page p WITH (NOLOCK) ON np.PageID = p.PageID
		LEFT JOIN dbo.Item i WITH (NOLOCK) ON p.ItemID = i.ItemID
WHERE	r.NameResolvedID >= @StartID
AND		(r.NameResolvedID <= @EndID OR @EndID IS NULL)
GROUP BY r.NameResolvedID,
        r.ResolvedNameString
ORDER BY r.NameResolvedID

END
