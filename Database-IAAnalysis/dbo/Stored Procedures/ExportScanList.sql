
CREATE PROCEDURE [dbo].[ExportScanList]

AS

BEGIN

SET NOCOUNT ON

TRUNCATE TABLE dbo.ExportScanListItem
TRUNCATE TABLE dbo.ExportScanListOCLC
TRUNCATE TABLE dbo.ExportScanListDates
TRUNCATE TABLE dbo.ExportScanListAuthor

-- Get Item IDs of all SERIALS
INSERT	ExportScanListItem (ItemID, BHLItemID, BHLTitleID)
SELECT DISTINCT c.ItemID, i.ItemID, i.PrimaryTitleID
FROM	vwMarcControl c INNER JOIN rptCombined r
			ON c.ItemID = r.ItemID
		INNER JOIN BHLItem i 
			ON r.Identifier = i.BarCode
			AND i.ItemStatusID = 40
WHERE	SUBSTRING(c.MARCLeader, 8, 1) IN ('b', 's')
AND		r.Sponsor <> 'Google'

-- Title
UPDATE	ExportScanListItem
SET		Title = ma.SubFieldValue
FROM	ExportScanListItem i INNER JOIN vwMarcDetail ma
			ON i.ItemID = ma.ItemID
			AND ma.DataFieldTag = '245'
			AND ma.Code = 'a'

UPDATE	ExportScanListItem
SET		Title = Title + ' ' + mb.SubFieldValue
FROM	ExportScanListItem i INNER JOIN vwMarcDetail mb
			ON i.ItemID = mb.ItemID
			AND mb.DataFieldTag = '245'
			AND mb.Code = 'b'

UPDATE	ExportScanListItem
SET		Title = Title + ' ' + mc.SubFieldValue
FROM	ExportScanListItem i INNER JOIN vwMarcDetail mc
			ON i.ItemID = mc.ItemID
			AND mc.DataFieldTag = '245'
			AND mc.Code = 'c'

-- Publisher
UPDATE	ExportScanListItem
SET		Publisher = m.SubFieldValue
FROM	ExportScanListItem i INNER JOIN vwMarcDetail m
			ON i.ItemID = m.ItemID
			AND m.DataFieldTag = '260'
			AND m.Code = 'b'

-- Publisher Place
UPDATE	ExportScanListItem
SET		PublisherPlace = m.SubFieldValue
FROM	ExportScanListItem i INNER JOIN vwMarcDetail m
			ON i.ItemID = m.ItemID
			AND m.DataFieldTag = '260'
			AND m.Code = 'a'

-- Volume
UPDATE	ExportScanListItem
SET		Volume = it.Volume
FROM	ExportScanListItem i INNER JOIN Item it
			ON i.ItemID = it.ItemID

-- Call number (first check the 050 record, then the 090... use the 050 value if both exist)
UPDATE	ExportScanListItem
SET		CallNumber = ma.SubFieldValue + ISNULL(' ' + mb.SubFieldValue, '')
FROM	ExportScanListItem i INNER JOIN vwMarcDetail ma
			ON i.ItemID = ma.ItemID
			AND ma.DataFieldTag = '050' 
			AND ma.Code = 'a'
		LEFT JOIN dbo.vwMarcDetail mb
			ON i.ItemID = mb.ItemID
			AND mb.DataFieldTag = '050'
			AND mb.Code = 'b'

UPDATE	ExportScanListItem
SET		CallNumber = ma.SubFieldValue + ' ' + ISNULL(mb.SubFieldValue, '')
FROM	ExportScanListItem i INNER JOIN dbo.vwMarcDetail ma
			ON i.ItemID = ma.ItemID
			AND ma.DataFieldTag = '090' 
			AND ma.Code = 'a'
		LEFT JOIN dbo.vwMarcDetail mb
			ON i.ItemID = mb.ItemID
			AND mb.DataFieldTag = '090'
			AND mb.Code = 'b'
WHERE	i.CallNumber IS NULL

-- OCLC - check 035a and 010o (the most likely places to find these)
INSERT	dbo.ExportScanListOCLC
SELECT	i.ItemID, 
		CASE WHEN CHARINDEX(' ', m.SubFieldValue) > 0
			THEN LEFT(m.SubFieldValue, CHARINDEX(' ', m.SubFieldValue) - 1)
			ELSE m.SubFieldValue END AS o035,
		CASE WHEN CHARINDEX(' ', m2.SubFieldValue) > 0
			THEN LEFT(m2.SubFieldValue, CHARINDEX(' ', m2.SubFieldValue) - 1)
			ELSE m2.SubFieldValue END AS o010
FROM	ExportScanListItem i 
		LEFT JOIN (SELECT * FROM dbo.vwMarcDetail
					WHERE DataFieldTag = '035' AND Code = 'a' AND 
					(SubFieldValue LIKE '(OCoLC)%' OR SubFieldValue LIKE 'ocm%' OR SubFieldValue LIKE 'ocn%' OR SubFieldValue LIKE 'on%')
					) m
			ON i.ItemID = m.ItemID
		LEFT JOIN (SELECT * FROM dbo.vwMarcDetail
					WHERE DataFieldTag = '010' AND Code = 'o') m2
			ON i.ItemID = m2.ItemID

UPDATE	ExportScanListItem
SET		OCLC = ISNULL(COALESCE(CONVERT(NVARCHAR(30), dbo.fnFilterString(o.o035, '[0-9]', '')), 
					CONVERT(NVARCHAR(30), dbo.fnFilterString(o.o010, '[0-9]', ''))), '')
FROM	ExportScanListItem i INNER JOIN dbo.ExportScanListOCLC o
			ON i.ItemID = o.ItemID

-- OCLC - now check MARC control 001 record (not too many of these)
UPDATE	ExportScanListItem
SET		OCLC = ISNULL(CONVERT(NVARCHAR(30), CONVERT(INT, dbo.fnFilterString(mc.value, '[0-9]', ''))), '')
FROM	ExportScanListItem i
		LEFT JOIN (SELECT * FROM dbo.vwMarcControl WHERE tag = '001' AND [value] NOT LIKE 'Catkey%') mc
			ON i.ItemID = mc.ItemID
		LEFT JOIN (SELECT * FROM dbo.vwMarcControl WHERE tag = '003' AND [value] = 'OCoLC') mc2
			ON i.ItemID = mc2.ItemID
WHERE	(mc.[Value] LIKE 'oc%' OR mc.[Value] LIKE 'on%' OR mc2.[value] = 'OCoLC')
AND		i.OCLC <> ''

-- Chronology
INSERT	dbo.ExportScanListDates
SELECT	i.ItemID,
		StartYear = CASE WHEN ISNUMERIC(SUBSTRING(c.[Value], 8, 4)) = 1 THEN SUBSTRING(c.[Value], 8, 4) ELSE '' END,
		EndYear = CASE WHEN ISNUMERIC(SUBSTRING(c.[Value], 12, 4)) = 1 THEN SUBSTRING(c.[Value], 12, 4) ELSE '' END
FROM	ExportScanListItem i INNER JOIN dbo.vwMarcControl c
			ON i.ItemID = c.ItemID
WHERE	c.Tag = '008' 

UPDATE	ExportScanListItem
SET		Chronology = CASE 
					WHEN d.EndYear = '' OR d.EndYear = '9999' THEN d.StartYear
					WHEN d.StartYear = '' OR d.StartYear = '9999' THEN d.EndYear
					ELSE d.StartYear + '-' + d.EndYear
					END
FROM	ExportScanListItem i INNER JOIN dbo.ExportScanListDates d
			ON i.ItemID = d.ItemID

-- Author
INSERT	dbo.ExportScanListAuthor
SELECT	i.ItemID, MIN(m.MarcDataFieldID) AS MarcDataFieldID
FROM	ExportScanListItem i INNER JOIN vwMarcDetail m
			ON i.ItemID = m.ItemID
			AND m.DataFieldTag IN ('100', '110', '111', '700', '710', '711')
GROUP BY i.ItemID

UPDATE	ExportScanListItem
SET		Author = m.SubFieldValue
FROM	ExportScanListItem i INNER JOIN dbo.ExportScanListAuthor a
			ON i.ItemID = a.ItemID
		INNER JOIN vwMarcDetail m
			ON a.MarcDataFieldID = m.MarcDataFieldID
			AND m.Code = 'a'

UPDATE	ExportScanListItem
SET		Author = Author + ' ' + m.SubFieldValue
FROM	ExportScanListItem i INNER JOIN dbo.ExportScanListAuthor a
			ON i.ItemID = a.ItemID
		INNER JOIN vwMarcDetail m
			ON a.MarcDataFieldID = m.MarcDataFieldID
			AND m.Code = 'b'

UPDATE	ExportScanListItem
SET		Author = Author + ' ' + m.SubFieldValue
FROM	ExportScanListItem i INNER JOIN dbo.ExportScanListAuthor a
			ON i.ItemID = a.ItemID
		INNER JOIN vwMarcDetail m
			ON a.MarcDataFieldID = m.MarcDataFieldID
			AND m.Code = 'c'

UPDATE	ExportScanListItem
SET		Author = Author + ' ' + m.SubFieldValue
FROM	ExportScanListItem i INNER JOIN dbo.ExportScanListAuthor a
			ON i.ItemID = a.ItemID
		INNER JOIN vwMarcDetail m
			ON a.MarcDataFieldID = m.MarcDataFieldID
			AND m.Code = 'd'

-- Final result
SELECT	BHLItemID AS ItemID,
		BHLTitleID AS TitleID,
		REPLACE(LocalNumber, '|', ' ') AS [Local Number],
		REPLACE(OCLC, '|', ' ') AS OCLC,
		REPLACE(Title, '|', ' ') AS Title,
		REPLACE(Author, '|', ' ') AS Author,
		REPLACE(Volume, '|', ' ') AS Volume,
		REPLACE(Chronology, '|', ' ') AS Chronology,
		REPLACE(CallNumber, '|', ' ') AS [Call Number],
		REPLACE(Publisher, '|', ' ') AS Publisher,
		REPLACE(PublisherPlace, '|', ' ') AS [Publisher Place]
FROM	ExportScanListItem
ORDER BY Title, Volume

SET NOCOUNT OFF

END

