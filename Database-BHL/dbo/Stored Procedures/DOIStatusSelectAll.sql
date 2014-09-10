
CREATE PROCEDURE [dbo].[DOIStatusSelectAll]

AS

BEGIN

SET NOCOUNT ON

SELECT	s.DOIStatusID, 
		s.DOIStatusName, 
		s.DOIStatusDescription,
		SUM(CASE WHEN d.DOIID IS NULL THEN 0 ELSE 1 END) AS NumberDOIs
FROM	dbo.DOIStatus s LEFT JOIN dbo.DOI d
			ON s.DOIStatusID = d.DOIStatusID
GROUP BY
		s.DOIStatusID, s.DOIStatusName, s.DOIStatusDescription
ORDER BY s.DOIStatusID

END

