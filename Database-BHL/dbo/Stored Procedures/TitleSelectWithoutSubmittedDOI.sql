
CREATE PROCEDURE [dbo].[TitleSelectWithoutSubmittedDOI]

@NumberToReturn int = 10

AS

BEGIN

SET NOCOUNT ON

DECLARE @NumToReturn int
SET @NumToReturn = @NumberToReturn

DECLARE @MaxDate DATETIME
SELECT @MaxDate = GETDATE() - 14 -- days

SELECT DISTINCT TOP (@NumToReturn)
		ISNULL(d.DOIID, 0) AS DOIID,
		ISNULL(d.DOIEntityTypeID, 0) AS DOIEntityTypeID,
		t.TitleID AS EntityID,
		ISNULL(d.DOIStatusID, 0) AS DOIStatusID,
		ISNULL(d.DOIName, '') AS DOIName
FROM	dbo.Title t LEFT JOIN dbo.DOI d
			ON t.TitleID = d.EntityID
			AND d.DOIEntityTypeID = 10 -- Title
		INNER JOIN dbo.TitleItem ti ON t.TitleID = ti.TitleID
		INNER JOIN dbo.SearchCatalog c ON ti.TitleID = c.TitleID AND ti.ItemID = c.ItemID
WHERE	t.PublishReady = 1
AND		(d.DOIStatusID = 10 -- No DOI
OR		d.DOIStatusID = 20 -- DOI Assigned (but not submitted)
OR		d.DOIStatusID = 30 -- Pending Resubmit
OR		d.DOIStatusID = 40 -- Batch ID Assigned
OR		d.DOIID IS NULL)
AND		t.CreationDate <= @MaxDate -- Only select titles older than specified # of days
AND		c.HasLocalContent = 1 -- Make sure that Page records exist (no items without scans)
AND		t.BibliographicLevelID IN (1, 4) -- Monographic component part, Monograph/Item
AND		t.FullTitle NOT LIKE '%Supplementary material in Charles Darwin''s copy%'

END
