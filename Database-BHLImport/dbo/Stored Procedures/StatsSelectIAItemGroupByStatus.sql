
CREATE PROCEDURE [dbo].[StatsSelectIAItemGroupByStatus]
AS
BEGIN

SET NOCOUNT ON

-- All IA items grouped by status
SELECT	i.ItemStatusID, 
		s.Status, 
		s.Description, 
		COUNT(*) AS [Number Of Items]
FROM	dbo.IAItem i INNER JOIN dbo.IAItemStatus s
			ON i.ItemStatusID = s.ItemStatusID
GROUP BY
		i.ItemStatusID, s.Status, s.Description
ORDER BY 
		i.ItemStatusID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure StatsSelectIAItemGroupByStatus. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

END