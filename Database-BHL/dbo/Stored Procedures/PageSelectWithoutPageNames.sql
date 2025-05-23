CREATE PROCEDURE [dbo].[PageSelectWithoutPageNames]

AS 

-- Select pages with NULL [LastPageNameLookupDate] values that are related
-- to items with NON-NULL [LastPageNameLookupDate] values.  The data set
-- returned will include individual pages added to items after the initial
-- page processing was completed for the items.

SET NOCOUNT ON

SELECT	p.[PageID],
		p.[FileNamePrefix]
FROM	[dbo].[Page] p
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		INNER JOIN [dbo].[Item] i ON ip.[ItemID] = i.[ItemID]
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
WHERE	p.[LastPageNameLookupDate] IS NULL
AND		b.[LastPageNameLookupDate] IS NOT NULL
AND		p.Active = 1
AND		i.ItemStatusID = 40

UNION

SELECT	p.[PageID],
		p.[FileNamePrefix]
FROM	[dbo].[Page] p
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		INNER JOIN [dbo].[Item] i ON ip.[ItemID] = i.[ItemID]
		INNER JOIN dbo.Segment s ON i.ItemID = s.ItemID
WHERE	p.[LastPageNameLookupDate] IS NULL
AND		s.[LastPageNameLookupDate] IS NOT NULL
AND		p.Active = 1
AND		i.ItemStatusID IN (30, 40)

ORDER BY
		p.FileNamePrefix ASC

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageSelectWithoutPageNames. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
