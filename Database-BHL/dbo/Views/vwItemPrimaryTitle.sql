SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- *** ---

CREATE VIEW [dbo].[vwItemPrimaryTitle]
AS
SELECT	i.ItemID,
		it.TitleID
FROM	dbo.Item i
		INNER JOIN dbo.ItemTitle it ON i.ItemID = it.ItemID
WHERE	it.IsPrimary = 1

GO
