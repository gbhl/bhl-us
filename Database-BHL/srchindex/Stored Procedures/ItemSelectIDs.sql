CREATE PROCEDURE [srchindex].[ItemSelectIDs]

@StartID int

AS 

BEGIN

SET NOCOUNT ON

SELECT DISTINCT 
		i.ItemID 
FROM	dbo.Item i
		INNER JOIN dbo.TitleItem ti ON i.ItemID = ti.ItemiD
		INNER JOIN dbo.Title t ON ti.TitleID = t.TitleID
WHERE	i.ItemStatusID = 40
AND		t.PublishReady = 1
AND		i.ItemID >= @StartID
ORDER BY i.ItemID

END
