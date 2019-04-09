CREATE PROCEDURE [srchindex].[ItemSelectForTitle]

@TitleID int

AS 

BEGIN

SET NOCOUNT ON

-- Return IDs of the published items for the specified title
SELECT	i.ItemID
FROM	dbo.TitleItem ti 
		INNER JOIN dbo.Item i ON ti.ItemID = i.ItemID
		INNER JOIN dbo.Title t ON ti.TitleID = t.TitleID
WHERE	ti.TitleID = @TitleID
AND		i.ItemStatusID = 40

END
