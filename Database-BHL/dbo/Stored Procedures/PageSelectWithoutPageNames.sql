
CREATE PROCEDURE [dbo].[PageSelectWithoutPageNames]

AS 

-- Select pages with NULL [LastPageNameLookupDate] values that are related
-- to items with NON-NULL [LastPageNameLookupDate] values.  The data set
-- returned will include individual pages added to items after the initial
-- page processing was completed for the items.

SET NOCOUNT ON

SELECT 
	p.[PageID],
	p.[FileNamePrefix]
FROM [dbo].[Page] p -- WITH (INDEX(IX_Page_LastPageNameLookupDate)) 
	INNER JOIN [dbo].[Item] i
		ON p.[ItemID] = i.[ItemID]
WHERE p.[LastPageNameLookupDate] IS NULL
AND	i.[LastPageNameLookupDate] IS NOT NULL
AND	p.Active = 1
AND	i.ItemStatusID = 40
ORDER BY
	p.ItemID ASC,
	p.[SequenceOrder] ASC

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageSelectWithoutPageNames. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
