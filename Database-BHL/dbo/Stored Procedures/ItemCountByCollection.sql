SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ItemCountByCollection]
	@CollectionID int
AS

BEGIN

SET NOCOUNT ON

SELECT	COUNT(*)
FROM	dbo.Item i 
		INNER JOIN dbo.ItemCollection ic ON i.ItemID = ic.ItemID
		INNER JOIN dbo.ItemTitle it ON i.ItemID = it.ItemID AND it.IsPrimary = 1
		INNER JOIN dbo.Title t ON it.TitleID = t.TitleID
WHERE	ic.CollectionID = @CollectionID
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1

END


GO
