
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
	[FullTitle] [ntext] NOT NULL,
	[ShortTitle] [nvarchar](255) NULL,
	[UniformTitle] [nvarchar](255) NULL,
	[SortTitle] [nvarchar](65) NULL,
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
SELECT	MARCBibID = REPLACE(Leader, ' ', 'x'),
		MARCLeader = Leader,
		''
FROM	dbo.Marc 
WHERE	MarcID = @MarcID

-- Add the bibliographic level
UPDATE	#tmpTitle
SET		BibliographicLevelID = b.BibliographicLevelID
FROM	#tmpTitle t INNER JOIN dbo.BibliographicLevel b
			ON SUBSTRING(t.MarcLeader, 8, 1) = b.MARCCode

-- Get the start year, end year, and language code from the MARC control data
UPDATE	#tmpTitle
SET		StartYear = CASE WHEN ISNUMERIC(SUBSTRING(c.[Value], 8, 4)) = 1 THEN SUBSTRING(c.[Value], 8, 4) ELSE NULL END,
		EndYear = CASE WHEN ISNUMERIC(SUBSTRING(c.[Value], 12, 4)) = 1 THEN SUBSTRING(c.[Value], 12, 4) ELSE NULL END,
		LanguageCode = SUBSTRING(c.[Value], 36, 3)
FROM	dbo.vwMarcControl c
WHERE	c.Tag = '008' 
AND		c.MarcID = @MarcID

-- If the language code is unrecognized, replace it with NULL.  In
-- the vast majority of cases, the code is unrecognized because it is
-- mal-formed in the MARC.
UPDATE	#tmpTitle
SET		LanguageCode = NULL
FROM	#tmpTitle t LEFT JOIN dbo.Language l ON t.LanguageCode = l.LanguageCode
WHERE	l.LanguageCode IS NULL

-- Get the publication titles
UPDATE	#tmpTitle
SET		ShortTitle = df.SubFieldValue
FROM	dbo.vwMarcDataField df
WHERE	df.DataFieldTag = '245'
AND		df.Code = 'a'
AND		df.MarcID = @MarcID

-- Get the uniform title (stored in either MARC 130 or MARC 240)
UPDATE	#tmpTitle
SET		UniformTitle = df.SubFieldValue
FROM	dbo.vwMarcDataField df
WHERE	df.DataFieldTag in ('130', '240')
AND		df.Code = 'a'
AND		df.MarcID = @MarcID

-- Full Title
UPDATE	#tmpTitle
SET		FullTitle = dfA.SubFieldValue + ' ' + 
					ISNULL(dfB.SubFieldValue, '')-- + ' ' + 
--					ISNULL(dfC.SubFieldValue, '')
FROM	dbo.Marc m INNER JOIN dbo.vwMarcDataField dfA
			ON m.MarcID = dfA.MarcID
			AND dfA.DataFieldTag = '245' 
			AND dfA.Code = 'a'
		LEFT JOIN dbo.vwMarcDataField dfB
			ON m.MarcID = dfB.MarcID
			AND dfB.DataFieldTag = '245'
			AND dfB.Code = 'b'
--		LEFT JOIN dbo.vwMarcDataField dfC
--			ON m.MarcID = dfC.MarcID
--			AND dfC.DataFieldTag = '245'
--			AND dfC.Code = 'c'
WHERE	m.MarcID = @MarcID

-- Part Number and Part Name
UPDATE	#tmpTitle
SET		PartNumber = df.SubFieldValue
FROM	dbo.vwMarcDataField df
WHERE	df.DataFieldTag = '245'
AND		df.Code = 'n'
AND		df.MarcID = @MarcID

UPDATE	#tmpTitle
SET		PartName = df.SubFieldValue
FROM	dbo.vwMarcDataField df
WHERE	df.DataFieldTag = '245'
AND		df.Code = 'p'
AND		df.MarcID = @MarcID

-- Get datafield 260/264 information
UPDATE	#tmpTitle
SET		Datafield_260_a = df.SubFieldValue
FROM	dbo.vwMarcDataField df
WHERE	(df.DataFieldTag = '260' OR (df.DataFieldTag = '264' AND df.Indicator2 = '1'))
AND		df.Code = 'a'
AND		df.MarcID = @MarcID

UPDATE	#tmpTitle
SET		Datafield_260_b = df.SubFieldValue
FROM	dbo.vwMarcDataField df
WHERE	(df.DataFieldTag = '260' OR (df.DataFieldTag = '264' AND df.Indicator2 = '1'))
AND		df.Code = 'b'
AND		df.MarcID = @MarcID

UPDATE	#tmpTitle
SET		Datafield_260_c = df.SubFieldValue
FROM	dbo.vwMarcDataField df
WHERE	(df.DataFieldTag = '260' OR (df.DataFieldTag = '264' AND df.Indicator2 = '1'))
AND		df.Code = 'c'
AND		df.MarcID = @MarcID

-- Get publication details
UPDATE	#tmpTitle
SET		PublicationDetails = ISNULL(Datafield_260_a, '') + ISNULL(Datafield_260_b, '') + ISNULL(Datafield_260_c, '')

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
SET		EditionStatement = t.SubFieldValue
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

-- Get the sort titles for all titles
-- Remove keywords from the full title
UPDATE	#tmpTitle
SET		SortTitle = SUBSTRING(
						LTRIM(RTRIM(
						REPLACE(
						REPLACE(
						REPLACE(
						REPLACE(
						REPLACE(
						REPLACE(CONVERT(NVARCHAR(4000), FullTitle), 
							' A ', ' '),
							' An ', ' '),
							' Les ', ' '),
							' Las ', ' '),
							' Los ', ' '),
							' L'' ', ' ')
						))
					, 1, 65)

-- Remove keywords from the beginning of the sort titles
UPDATE	#tmpTitle
SET		SortTitle = CASE 
					WHEN SUBSTRING(SortTitle, 1, 4) = 'The ' THEN SUBSTRING(SortTitle, 5, 60)
					WHEN SUBSTRING(SortTitle, 1, 2) = 'A ' THEN SUBSTRING(SortTitle, 3, 60)
					WHEN SUBSTRING(SortTitle, 1, 3) = 'An ' THEN SUBSTRING(SortTitle, 4, 60)
					WHEN SUBSTRING(SortTitle, 1, 4) = 'Les ' THEN SUBSTRING(SortTitle, 5, 60)
					WHEN SUBSTRING(SortTitle, 1, 4) = 'Las ' THEN SUBSTRING(SortTitle, 5, 60)
					WHEN SUBSTRING(SortTitle, 1, 4) = 'Los ' THEN SUBSTRING(SortTitle, 5, 60)
					WHEN SUBSTRING(SortTitle, 1, 3) = 'L'' ' THEN SUBSTRING(SortTitle, 4, 60)
					WHEN SUBSTRING(SortTitle, 1, 3) = '...' THEN LTRIM(SUBSTRING(SortTitle, 4, 60))
					WHEN SUBSTRING(SortTitle, 1, 1) = '[' THEN SUBSTRING(SortTitle, 2, 60)
					ELSE SUBSTRING(SortTitle, 1, 60)
					END

-- =======================================================================
-- Deliver the final result set
SELECT	[MARCBibID],
		[MARCLeader],
		[BibliographicLevelID],
		[FullTitle],
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
