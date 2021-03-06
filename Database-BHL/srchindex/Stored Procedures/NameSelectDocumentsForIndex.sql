SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [srchindex].[NameSelectDocumentsForIndex]

@StartID int,
@EndID int = NULL

AS 

BEGIN

SET NOCOUNT ON

SELECT TOP 200000
		NameResolvedID,
		ResolvedNamestring
INTO	#NameResolved
FROM	dbo.NameResolved WITH (NOLOCK)
WHERE	NameResolvedID >= @StartID
AND		(NameResolvedID <= @EndID OR @EndID IS NULL)
ORDER BY NameResolvedID

SELECT	r.NameResolvedID,
		r.ResolvedNameString,
		SUM(CASE WHEN i.ItemStatusID = 40 AND p.Active = 1 AND n.IsActive = 1 THEN 1 ELSE 0 END) AS NameCount
FROM	#NameResolved r
		INNER JOIN dbo.Name n WITH (NOLOCK) ON r.NameResolvedID = n.NameResolvedID
		LEFT JOIN dbo.NamePage np WITH (NOLOCK) ON n.NameID = np.NameID
		LEFT JOIN dbo.Page p WITH (NOLOCK) ON np.PageID = p.PageID
		LEFT JOIN dbo.ItemPage ip WITH (NOLOCK) ON p.PageID = ip.PageID
		LEFT JOIN dbo.Item i WITH (NOLOCK) ON ip.ItemID = i.ItemID
GROUP BY r.NameResolvedID,
        r.ResolvedNameString
ORDER BY r.NameResolvedID

END


GO
