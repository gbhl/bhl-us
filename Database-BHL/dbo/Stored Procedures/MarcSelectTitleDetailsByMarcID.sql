CREATE PROCEDURE [dbo].[MarcSelectTitleDetailsByMarcID]

@MarcID int

AS
BEGIN

SET NOCOUNT ON

-- =======================================================================
-- Build temp table

CREATE TABLE #tmpTitle (
	[MARCBibID] [nvarchar](50) NOT NULL,
	[MARCLeader] [nvarchar](24) NULL,
	[BibliographicLevelID] [int] NULL,
	[MaterialTypeID] [int] NULL,
	[FullTitle] [ntext] NOT NULL,
	[ShortTitle] [nvarchar](255) NULL,
	[UniformTitle] [nvarchar](255) NULL,
	[SortTitle] [nvarchar](60) NULL,
	[CallNumber] [nvarchar](100) NULL,
	[PublicationDetails] [nvarchar](255) NULL,
	[StartYear] [smallint] NULL,
	[EndYear] [smallint] NULL,
	[Datafield_260_a] [nvarchar](150) NULL,
	[Datafield_260_b] [nvarchar](255) NULL,
	[Datafield_260_c] [nvarchar](100) NULL,
	[LanguageCode] [nvarchar](10) NULL,
	[OriginalCatalogingSource] [nvarchar](100) NULL,
	[EditionStatement] [nvarchar](450) NULL,
	[CurrentPublicationFrequency] [nvarchar](100) NULL,
	[PartNumber] [nvarchar](255) NULL,
	[PartName] [nvarchar](255) NULL
	)


-- =======================================================================
-- Populate the temp table

-- Get the MARC leader and MARC BIB ID
INSERT	#tmpTitle (MARCBibID, MARCLeader, FullTitle)
SELECT	MARCBibID = REPLACE(REPLACE(Leader, ' ', 'x'), '|', 'x'),
		MARCLeader = Leader,
		''
FROM	dbo.Marc 
WHERE	MarcID = @MarcID

-- Add the bibliographic level
UPDATE	#tmpTitle
SET		BibliographicLevelID = b.BibliographicLevelID
FROM	#tmpTitle t INNER JOIN dbo.BibliographicLevel b
			ON SUBSTRING(t.MarcLeader, 8, 1) = b.MARCCode

-- Add the material type
UPDATE	#tmpTitle
SET		MaterialTypeID = m.MaterialTypeID
FROM	#tmpTitle t INNER JOIN dbo.MaterialType m
			ON SUBSTRING(t.MarcLeader, 7, 1) = m.MARCCode

-- Get the start year, end year, and language code from the MARC control data
UPDATE	#tmpTitle
SET		StartYear = CASE WHEN ISNUMERIC(SUBSTRING(c.[Value], 8, 4)) = 1 THEN SUBSTRING(c.[Value], 8, 4) ELSE NULL END,
		EndYear = CASE WHEN ISNUMERIC(SUBSTRING(c.[Value], 12, 4)) = 1 THEN SUBSTRING(c.[Value], 12, 4) ELSE NULL END,
		LanguageCode = SUBSTRING(c.[Value], 36, 3)
FROM	dbo.vwMarcControl c
WHERE	c.Tag = '008' 
AND		c.MarcID = @MarcID

-- Make sure the start and end years are within the valid ranges
UPDATE	#tmpTitle
SET		StartYear = CASE WHEN ((StartYear>=1400 AND StartYear<=2025) OR StartYear IS NULL) THEN StartYear ELSE NULL END,
		EndYear = CASE WHEN ((EndYear>=1400 AND EndYear<=2025) OR EndYear IS NULL) THEN EndYear ELSE NULL END

-- If the language code is unrecognized, replace it with NULL.  In
-- the vast majority of cases, the code is unrecognized because it is
-- mal-formed in the MARC.
UPDATE	#tmpTitle
SET		LanguageCode = NULL
FROM	#tmpTitle t LEFT JOIN dbo.Language l ON t.LanguageCode = l.LanguageCode
WHERE	l.LanguageCode IS NULL

-- Get the publication titles
UPDATE	#tmpTitle
SET		ShortTitle = SUBSTRING(df.SubFieldValue, 1, 255)
FROM	dbo.vwMarcDataField df
WHERE	df.DataFieldTag = '245'
AND		df.Code = 'a'
AND		df.MarcID = @MarcID

-- Get the uniform title (stored in either MARC 130 or MARC 240)
UPDATE	#tmpTitle
SET		UniformTitle = SUBSTRING(df.SubFieldValue, 1, 255)
FROM	dbo.vwMarcDataField df
WHERE	df.DataFieldTag in ('130', '240')
AND		df.Code = 'a'
AND		df.MarcID = @MarcID

-- Full Title
UPDATE	#tmpTitle
SET		FullTitle = LTRIM(RTRIM(dfA.SubFieldValue + ' ' +  ISNULL(dfB.SubFieldValue, '')))
FROM	dbo.Marc m INNER JOIN dbo.vwMarcDataField dfA
			ON m.MarcID = dfA.MarcID
			AND dfA.DataFieldTag = '245' 
			AND dfA.Code = 'a'
		LEFT JOIN dbo.vwMarcDataField dfB
			ON m.MarcID = dfB.MarcID
			AND dfB.DataFieldTag = '245'
			AND dfB.Code = 'b'
WHERE	m.MarcID = @MarcID

-- Strip forward slashes, commas, and semicolons from the end of title strings
UPDATE	#tmpTitle
SET		FullTitle = RTRIM(SUBSTRING(CONVERT(nvarchar(max), FullTitle), 1, LEN(CONVERT(nvarchar(max), FullTitle)) - 1))
WHERE	SUBSTRING(REVERSE(RTRIM(CONVERT(nvarchar(max), FullTitle))), 1, 1) in ('/', ',', ';')

UPDATE	#tmpTitle
SET		ShortTitle = RTRIM(SUBSTRING(ShortTitle, 1, LEN(ShortTitle) - 1))
WHERE	SUBSTRING(REVERSE(RTRIM(ShortTitle)), 1, 1) in ('/', ',', ';')

-- Part Number and Part Name
UPDATE	#tmpTitle
SET		PartNumber = SUBSTRING(df.SubFieldValue, 1, 255)
FROM	dbo.vwMarcDataField df
WHERE	df.DataFieldTag = '245'
AND		df.Code = 'n'
AND		df.MarcID = @MarcID

UPDATE	#tmpTitle
SET		PartName = SUBSTRING(df.SubFieldValue, 1, 255)
FROM	dbo.vwMarcDataField df
WHERE	df.DataFieldTag = '245'
AND		df.Code = 'p'
AND		df.MarcID = @MarcID;

-- Get datafield 260/264 information
/*
IF     * 260 with blank ind 1 or ind 1=0 or ind 1=1
OR    ** 264 with blank ind 1 and ind 2=1
OR    ** 264 with blank ind 1 and ind 2=0
OR    ** 264 with blank ind 1 and ind 2=3
THEN *** take first subfield a, b and c and/or 3 to populate the BHL database
*/
-- Get IDs of the appropriate 260/264 Marc fields
WITH DataField (MarcID, MarcDataFieldID)
AS
(
	SELECT	df.MarcID, MIN(MarcDataFieldID) AS MarcDataFieldID
	FROM	dbo.vwMarcDataField df
	WHERE	((df.DataFieldTag = '260' AND df.Indicator1 IN ('', '0', '1'))
	OR		(df.DataFieldTag = '264' AND df.Indicator1 = '' AND df.Indicator2 IN ('0', '1', '3')))
	AND		df.MarcID = @MarcID
	GROUP BY df.MarcID
)
SELECT	d.MarcID, d.MarcDataFieldID, MIN(v.MarcSubFieldID) AS MarcSubFieldID, v.Code
INTO	#PublisherInfo
FROM	DataField d INNER JOIN dbo.vwMarcDataField v
			ON d.MarcDataFieldID = v.MarcDataFieldID
GROUP BY d.MarcID, d.MarcDataFieldID, v.Code

-- Get the 260/264 values
UPDATE	#tmpTitle
SET		Datafield_260_a = SUBSTRING(df.SubFieldValue, 1, 150)
FROM	#PublisherInfo p
		INNER JOIN dbo.vwMarcDataField df ON p.MarcID = df.MarcID AND p.MarcSubFieldID = df.MarcSubFieldID
WHERE	p.Code = 'a'

UPDATE	#tmpTitle
SET		Datafield_260_b = SUBSTRING(df.SubFieldValue, 1, 255)
FROM	#PublisherInfo p
		INNER JOIN dbo.vwMarcDataField df ON p.MarcID = df.MarcID AND p.MarcSubFieldID = df.MarcSubFieldID
WHERE	p.Code = 'b'

UPDATE	#tmpTitle
SET		Datafield_260_c = SUBSTRING(df.SubFieldValue, 1, 100)
FROM	#PublisherInfo p
		INNER JOIN dbo.vwMarcDataField df ON p.MarcID = df.MarcID AND p.MarcSubFieldID = df.MarcSubFieldID
WHERE	p.Code = 'c'

UPDATE	#tmpTitle
SET		Datafield_260_c = SUBSTRING(df.SubFieldValue, 1, 100)
FROM	#PublisherInfo p
		INNER JOIN dbo.vwMarcDataField df ON p.MarcID = df.MarcID AND p.MarcSubFieldID = df.MarcSubFieldID
WHERE	p.Code = '3'
AND		ISNULL(Datafield_260_c, '') = ''

DROP TABLE #PublisherInfo

-- Remove start and end brackets ( [ ] ) from publication information
UPDATE	#tmpTitle
SET		Datafield_260_a = SUBSTRING(Datafield_260_a, 2, LEN(Datafield_260_a) - 1),
        Datafield_260_b = CASE WHEN ISNULL(Datafield_260_c, '') = '' THEN SUBSTRING(Datafield_260_b, 1, LEN(Datafield_260_b) - 1) ELSE Datafield_260_b END,
        Datafield_260_c = CASE WHEN ISNULL(Datafield_260_c, '') <> '' THEN SUBSTRING(Datafield_260_c, 1, LEN(Datafield_260_c) - 1) ELSE Datafield_260_c END
WHERE	LEFT(Datafield_260_a, 1) = '['
AND     CHARINDEX(']', Datafield_260_a, 1) = 0
AND     (
			(
			RIGHT(Datafield_260_c, 1) = ']'
			AND CHARINDEX('[', Datafield_260_c, 1) = 0
			)
		OR
			(
			RIGHT(Datafield_260_b, 1) = ']'
			AND CHARINDEX('[', Datafield_260_b, 1) = 0
			AND ISNULL(Datafield_260_c, '') = ''
			)
		)

-- Get publication details
UPDATE	#tmpTitle
SET		PublicationDetails = SUBSTRING(ISNULL(Datafield_260_a, '') + ISNULL(Datafield_260_b, '') + 
			ISNULL(Datafield_260_c, ''), 1, 255)

-- Get the call number (first check the 050 record, then the 090... use the 050 value if both exist)
UPDATE	#tmpTitle
SET		CallNumber = dfA.SubFieldValue + ' ' + ISNULL(dfB.SubFieldValue, '')
FROM	dbo.Marc m INNER JOIN dbo.vwMarcDataField dfA
			ON m.MarcID = dfA.MarcID
			AND dfA.DataFieldTag = '050' 
			AND dfA.Code = 'a'
		LEFT JOIN dbo.vwMarcDataField dfB
			ON m.MarcID = dfB.MarcID
			AND dfB.DataFieldTag = '050'
			AND dfB.Code = 'b'
WHERE	m.MarcID = @MarcID

UPDATE	#tmpTitle
SET		CallNumber = CASE WHEN CallNumber IS NULL 
					THEN dfA.SubFieldValue + ' ' + ISNULL(dfB.SubFieldValue, '')
					ELSE CallNumber
					END
FROM	dbo.Marc m INNER JOIN dbo.vwMarcDataField dfA
			ON m.MarcID = dfA.MarcID
			AND dfA.DataFieldTag = '090' 
			AND dfA.Code = 'a'
		LEFT JOIN dbo.vwMarcDataField dfB
			ON m.MarcID = dfB.MarcID
			AND dfB.DataFieldTag = '090'
			AND dfB.Code = 'b'
WHERE	m.MarcID = @MarcID

-- Get the Original Cataloging Source (only pull the original agency, not any
-- modifying agencies)
UPDATE	#tmpTitle
SET 	OriginalCatalogingSource = m.SubFieldValue
FROM	dbo.vwMarcDataField m
WHERE	m.DataFieldTag = '040'
AND		m.Code = 'a'
AND		m.MarcID = @MarcID

-- Get the Edition Statement
UPDATE	#tmpTitle
SET		EditionStatement = SUBSTRING(t.SubFieldValue, 1, 450)
FROM	dbo.Marc m INNER JOIN (
				SELECT	MarcID, MarcDataFieldID, 
						LTRIM(ISNULL(MIN([a]), '') + ' ' + ISNULL(MIN([b]), '')) AS SubFieldValue
				FROM	(
						SELECT	MarcID, MarcDataFieldID, [a], [b]
						FROM	(SELECT * FROM dbo.vwMarcDataField
								WHERE DataFieldTag = '250' AND Code IN ('a', 'b')) AS z
						PIVOT	(MIN(SubFieldValue) FOR Code IN ([a], [b])) AS Pvt
						) X
				GROUP BY MarcID, MarcDataFieldID
				) t
			ON m.MarcID = t.MarcID
WHERE	m.MarcID = @MarcID

-- Get the Current Publication Frequency
UPDATE	#tmpTitle
SET		CurrentPublicationFrequency = m.SubFieldValue
FROM	dbo.vwMarcDataField m
WHERE	m.DataFieldTag = '310'
AND		m.Code = 'a'
AND		m.MarcID = @MarcID

-- =======================================================================

-- Use Indicator2 of the 245a field to build the appropriate SortTitle
UPDATE	#tmpTitle
SET		SortTitle = LTRIM(RTRIM(SUBSTRING(	FullTitle, 
											CASE WHEN ISNUMERIC(ISNULL(Indicator2, '')) = 1 THEN 
												CASE WHEN Indicator2 = '0' THEN 1 ELSE CONVERT(int, Indicator2) END
											ELSE 1 END, 
											60)
								))
FROM	dbo.vwMarcDataField m
WHERE	m.DataFieldTag = '245'
AND		m.Code = 'a'
AND		m.MarcID = @MarcID

-- =======================================================================
-- Deliver the final result set
SELECT	[MARCBibID],
		[MARCLeader],
		[BibliographicLevelID],
		[MaterialTypeID],
		SUBSTRING(CONVERT(nvarchar(max), [FullTitle]), 1, 2000) AS FullTitle,
		[ShortTitle],
		[UniformTitle],
		[SortTitle],
		[CallNumber],
		[PublicationDetails],
		[StartYear],
		CASE WHEN [EndYear] = 9999 THEN NULL ELSE [EndYear] END AS [EndYear],
		[Datafield_260_a],
		[Datafield_260_b],
		[Datafield_260_c],
		UPPER([LanguageCode]) AS LanguageCode,
		[OriginalCatalogingSource],
		[EditionStatement],
		[CurrentPublicationFrequency],
		[PartNumber],
		[PartName]
FROM	#tmpTitle

END
