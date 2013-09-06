
CREATE PROCEDURE [dbo].[MarcSelectTitleIdentifiersByMarcID]

@MarcID int

AS
BEGIN

SET NOCOUNT ON

-- =======================================================================
-- Build temp table

CREATE TABLE #tmpTitleIdentifier
	(
	[IdentifierName] [nvarchar](40) NOT NULL,
	[IdentifierValue] [nvarchar](125) NULL
	)

-- =======================================================================
-- Populate the temp table

-- Get the OCLC numbers from the 035a and 010o MARC fields (in most cases it's located in one
-- or the other of these)
INSERT INTO #tmpTitleIdentifier
SELECT	'OCLC',
		COALESCE(CONVERT(NVARCHAR(30), CONVERT(BIGINT, dbo.fnFilterString(m.subfieldvalue, '[0-9]', ''))), 
				CONVERT(NVARCHAR(30), CONVERT(BIGINT, dbo.fnFilterString(m2.subfieldvalue, '[0-9]', ''))))
FROM	dbo.Marc t 
		LEFT JOIN (SELECT * FROM dbo.vwMarcDataField 
					WHERE DataFieldTag = '035' AND code = 'a' AND 
					(SubFieldValue LIKE '(OCoLC)%' OR SubFieldValue LIKE 'ocm%' OR SubFieldValue LIKE 'ocn%' OR SubFieldValue LIKE 'on%')
					) m
			ON t.MarcID = m.MarcID
		LEFT JOIN (SELECT * FROM dbo.vwMarcDataField
					WHERE DataFieldTag = '010' AND Code = 'o') m2
			ON t.MarcID = m2.MarcID
WHERE	t.MarcID = @MarcID

-- Next check MARC control 001 record for the OCLC number (not too many of these)
INSERT INTO #tmpTitleIdentifier
SELECT	'OCLC',
		CONVERT(NVARCHAR(30), CONVERT(INT, dbo.fnFilterString(mc.value, '[0-9]', '')))
FROM	dbo.Marc t 
		LEFT JOIN (SELECT * FROM dbo.vwMarcControl WHERE tag = '001' AND [value] NOT LIKE 'Catkey%') mc
			ON t.MarcID = mc.MarcID
		LEFT JOIN (SELECT * FROM dbo.vwMarcControl WHERE tag = '003' AND [value] = 'OCoLC') mc2
			ON t.MarcID = mc2.MarcID
WHERE	(mc.[Value] LIKE 'oc%' OR mc.[Value] LIKE 'on%' OR mc2.[value] = 'OCoLC')
AND		NOT EXISTS (SELECT IdentifierValue FROM #tmpTitleIdentifier 
					WHERE IdentifierValue IS NOT NULL)
AND		t.MarcID = @MarcID

-- Get the Library Of Congress Control numbers
INSERT INTO #tmpTitleIdentifier
SELECT DISTINCT
		'DLC',
		LTRIM(RTRIM(m.SubFieldValue))
FROM	dbo.vwMarcDataField m
WHERE	DataFieldTag = '010'
AND		Code = 'a'
AND		m.MarcID = @MarcID

-- Get the ISBN identifiers
INSERT INTO #tmpTitleIdentifier
SELECT DISTINCT
		'ISBN',
		m.SubFieldValue
FROM	dbo.vwMarcDataField m
WHERE	m.DataFieldTag = '020'
AND		m.Code = 'a'
AND		m.MarcID = @MarcID

-- Get the ISSN identifiers
INSERT INTO #tmpTitleIdentifier
SELECT DISTINCT
		'ISSN',
		m.SubFieldValue
FROM	dbo.vwMarcDataField m
WHERE	m.DataFieldTag = '022'
AND		m.Code = 'a'
AND		m.MarcID = @MarcID

-- Get the CODEN codes
INSERT INTO #tmpTitleIdentifier
SELECT DISTINCT
		'CODEN',
		m.SubFieldValue
FROM	dbo.vwMarcDataField m
WHERE	m.DataFieldTag = '030'
AND		m.Code = 'a'
AND		m.MarcID = @MarcID

-- Get the National Library of Medicine call numbers
INSERT INTO #tmpTitleIdentifier
SELECT DISTINCT
		'NLM', 
		Z.SubFieldValue
FROM	(SELECT	MarcID, MarcDataFieldID, 
				LTRIM(ISNULL(MIN([a]), '') + ' ' + ISNULL(MIN([b]), '')) AS SubFieldValue
		FROM	(
				SELECT	MarcID, MarcDataFieldID, [a], [b]
				FROM	(SELECT * FROM dbo.vwMarcDataField 
						WHERE DataFieldTag = '060' AND Code in ('a', 'b')) AS m
				PIVOT	(MIN(SubFieldValue) FOR Code IN ([a], [b])) AS Pvt
				) X
		GROUP BY MarcID, MarcDataFieldID
		) Z
WHERE	MarcID = @MarcID

-- Get the National Agricultural Library call numbers
INSERT INTO #tmpTitleIdentifier
SELECT DISTINCT
		'NAL', 
		Z.SubFieldValue
FROM	(
		SELECT	MarcID, MarcDataFieldID, 
				LTRIM(ISNULL(MIN([a]), '') + ' ' + ISNULL(MIN([b]), '')) AS SubFieldValue
		FROM	(
				SELECT	MarcID, MarcDataFieldID, [a], [b]
				FROM	(SELECT * FROM dbo.vwMarcDataField 
						WHERE DataFieldTag = '070' AND Code in ('a', 'b')) AS m
				PIVOT	(MIN(SubFieldValue) FOR Code IN ([a], [b])) AS Pvt
				) X
		GROUP BY MarcID, MarcDataFieldID
		) Z
WHERE	MarcID = @MarcID

-- Get the Government Printing Office
INSERT INTO #tmpTitleIdentifier
SELECT DISTINCT
		'GPO',
		m.SubFieldValue
FROM	dbo.vwMarcDataField m
WHERE	m.DataFieldTag = '074'
AND		m.Code = 'a'
AND		m.MarcID = @MarcID

-- Get the Dewey Decimal Classifiers
INSERT INTO #tmpTitleIdentifier
SELECT DISTINCT
		'DDC',
		m.SubFieldValue
FROM	dbo.vwMarcDataField m
WHERE	m.DataFieldTag = '082'
AND		m.Code = 'a'
AND		m.MarcID = @MarcID

-- Get the Abbreviations
INSERT INTO #tmpTitleIdentifier
SELECT DISTINCT
		'Abbreviation',
		m.SubFieldValue
FROM	dbo.vwMarcDataField m
WHERE	m.DataFieldTag = '210'
AND		m.Code = 'a'
AND		m.MarcID = @MarcID

-- Get the WonderFetch identifiers (look for a MARC
-- 001 control record with a value including 'catkey')
INSERT INTO #tmpTitleIdentifier
SELECT DISTINCT 
		'WonderFetch',
		LTRIM(RTRIM(REPLACE(m.[Value], 'catkey', ''))) 
FROM	dbo.vwMarcControl m
WHERE	m.Tag = '001' 
AND		m.[Value] LIKE 'catkey%'
AND		m.MarcID = @MarcID

-- Get the non-OCLC and non-WonderFetch local identifiers from the 
-- MARC 001 control record
INSERT INTO #tmpTitleIdentifier
SELECT DISTINCT
		'MARC001',
		m.[Value]
FROM	dbo.vwMarcControl m
WHERE	m.Tag = '001'
AND		m.[Value] NOT LIKE 'catkey%'
AND		m.[Value] NOT LIKE 'oc%'
AND		m.MarcID = @MarcID

-- =======================================================================
-- Deliver the final result set
SELECT	ti.IdentifierID,
		t.IdentifierName,
		t.IdentifierValue
FROM	#tmpTitleIdentifier t INNER JOIN dbo.Identifier ti
			ON t.IdentifierName = ti.IdentifierName
WHERE	IdentifierValue IS NOT NULL

DROP TABLE #tmpTitleIdentifier

END



