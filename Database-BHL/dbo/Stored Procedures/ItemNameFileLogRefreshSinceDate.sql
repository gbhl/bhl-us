CREATE PROCEDURE [dbo].[ItemNameFileLogRefreshSinceDate]

@StartDate datetime

AS

BEGIN

SET NOCOUNT ON

DECLARE @ItemSourceIA int
SELECT @ItemSourceIA = ItemSourceID FROM dbo.ItemSource WHERE SourceName = 'Internet Archive'

-- Could do this in a single SQL statement, but breaking it down increases performance by 50x

SELECT	PageID, ItemID
INTO	#tmpPage
FROM	dbo.Page
WHERE	LastPageNameLookupDate > @StartDate

SELECT DISTINCT PageID, CreationDate, LastModifiedDate
INTO	#tmpPageName
FROM	dbo.NamePage np 
WHERE	PageID IN (SELECT PageID FROM #tmpPage)

SELECT DISTINCT i.ItemID
INTO	#tmpItem
FROM	#tmpPageName t 
		INNER JOIN dbo.ItemPage ip ON t.PageID = ip.PageID
		INNER JOIN dbo.Item i ON ip.ItemID = i.ItemID AND i.ItemSourceID = @ItemSourceIA
WHERE	t.CreationDate > @StartDate OR t.LastModifiedDate > @StartDate

-- Remove any items not contributed by BHL member libraries
DELETE	#tmpItem
WHERE	ItemID IN (
			SELECT	t.ItemID
			FROM	#tmpItem t 
					INNER JOIN dbo.Item i ON t.ItemID = i.ItemiD
					INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
					INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
					INNER JOIN dbo.Institution inst ON ii.InstitutionCode = inst.InstitutionCode
			WHERE	r.InstitutionRoleName IN ('Holding Institution', 'Contributor')
			GROUP BY t.ItemID HAVING SUM(CONVERT(int, inst.BHLMemberLibrary)) = 0)

-- Add new rows to table
INSERT INTO dbo.ItemNameFileLog (ItemID, DoUpload)
SELECT	t.ItemID, CASE WHEN i.ItemSourceID = 1 THEN 1 ELSE 0 END
FROM	#tmpItem t 
		INNER JOIN dbo.Item i ON t.ItemID = i.ItemID
		LEFT JOIN dbo.ItemNameFileLog l ON t.ItemID = l.ItemID
WHERE	l.ItemID IS NULL

-- Update existing rows in table
UPDATE	dbo.ItemNameFileLog
SET		DoCreate = 1,
		DoUpload = CASE WHEN i.ItemSourceID = 1 THEN 1 ELSE 0 END,
		LastModifiedDate = GETDATE()
FROM	#tmpItem t INNER JOIN dbo.Item i
			ON t.ItemID = i.ItemID
		INNER JOIN dbo.ItemNameFileLog l
			ON t.ItemID = l.ItemID

END

GO
