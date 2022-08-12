CREATE PROCEDURE [dbo].[DOIInsertEligibleEntities]

AS

/*
Insert into the DOI table entities (such as Titles and Segments) that are eligible
for DOI assignment.
*/

BEGIN

SET NOCOUNT ON

DECLARE @MaxDate DATETIME
SELECT @MaxDate = GETDATE() - 14 -- days

-- Year for sliding copyright window
DECLARE @YearLimit int
SELECT @YearLimit = DATEPART(year, GETDATE()) - 95

DECLARE @DOIStatusQueued int
SELECT @DOIStatusQueued = DOIStatusID FROM dbo.DOIStatus WHERE DOIStatusName = 'Queued'

DECLARE @DOIEntityTypeTitle int
SELECT @DOIEntityTypeTitle = DOIEntityTypeID FROM dbo.DOIEntityType WHERE DOIEntityTypeName = 'Title'

INSERT	dbo.DOI (DOIEntityTypeID, EntityID, DOIStatusID)
SELECT	DISTINCT
		@DOIEntityTypeTitle AS DOIEntityTypeID,
		t.TitleID AS EntityID,
		@DOIStatusQueued AS DOIStatusID
FROM	dbo.Title t
		LEFT JOIN dbo.DOI d ON t.TitleID = d.EntityID AND d.DOIEntityTypeID = @DOIEntityTypeTitle
		INNER JOIN dbo.ItemTitle it ON t.TitleID = it.TitleID
		INNER JOIN dbo.Book b ON it.ItemID = b.ItemID
		INNER JOIN dbo.SearchCatalog c ON it.TitleID = c.TitleID AND b.BookID = c.ItemID
WHERE	t.PublishReady = 1
AND		ISNULL(t.StartYear, 9999) < @YearLimit -- Only out-of-copyright titles
AND		t.CreationDate <= @MaxDate -- Only select titles older than specified # of days
AND		c.HasLocalContent = 1 -- Make sure that Page records exist (no items without scans)
AND		t.BibliographicLevelID IN (1, 4) -- Monographic component part, Monograph/Item
AND		t.FullTitle NOT LIKE '%Supplementary material in Charles Darwin''s copy%'
AND		d.DOIID IS NULL

END

GO
