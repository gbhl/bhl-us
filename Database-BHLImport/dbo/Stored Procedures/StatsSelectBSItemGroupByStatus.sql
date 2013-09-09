CREATE PROCEDURE [dbo].[StatsSelectBSItemGroupByStatus]
AS
BEGIN

SET NOCOUNT ON

-- All BioStor items grouped by status
SELECT	s.ItemStatusID,
		s.[Status],
		s.[Description],
		COUNT(*) AS [Number Of Items]
FROM	dbo.BSItem i INNER JOIN dbo.BSItemStatus s
			ON i.ItemStatusID = s.ItemStatusID
GROUP BY
		s.ItemStatusID,
		s.[Status],
		s.[Description]
ORDER BY
		s.ItemStatusID

END

