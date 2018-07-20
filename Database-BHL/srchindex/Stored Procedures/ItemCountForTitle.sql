CREATE PROCEDURE [srchindex].[ItemCountForTitle]

@TitleID int

AS 

BEGIN

SET NOCOUNT ON

-- Return the current number of published items for the specified title
SELECT	i.ItemID
FROM	dbo.TitleItem ti INNER JOIN dbo.Item i ON ti.ItemID = i.ItemID
WHERE	ti.TitleID = @TitleID
AND		i.ItemStatusID = 40

END
