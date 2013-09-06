
CREATE PROCEDURE [dbo].[MarcSelectAssociationsByMarcID]

@MarcID int

AS
BEGIN

SET NOCOUNT ON

-- =======================================================================
-- Build temp table

CREATE TABLE #tmpTitleAssociation(
	[Sequence] [int] NOT NULL,
	[MARCDataFieldID] [int] NOT NULL,
	[MARCTag] [nvarchar](20) NOT NULL,
	[MARCIndicator1] [nchar](1) NOT NULL DEFAULT (''),
	[MARCIndicator2] [nchar](1) NOT NULL DEFAULT (''),
	[Title] [nvarchar](500) NOT NULL DEFAULT (''),
	[Section] [nvarchar](500) NOT NULL DEFAULT (''),
	[Volume] [nvarchar](500) NOT NULL DEFAULT (''),
	[Heading] [nvarchar](500) NOT NULL DEFAULT (''),
	[Publication] [nvarchar](500) NOT NULL DEFAULT (''),
	[Relationship] [nvarchar](500) NOT NULL DEFAULT (''),
	[Active] bit NOT NULL DEFAULT(1)
	)

-- =======================================================================
-- Populate the temp table

-- Get 440 and 490 tag with an 'a' code
INSERT INTO #tmpTitleAssociation
SELECT	ROW_NUMBER() OVER (PARTITION BY m.MarcDataFieldID
							ORDER BY m.MarcSubFieldID) AS Sequence,
		m.MarcDataFieldID,
		m.DataFieldTag, 
		m.Indicator1 AS MARCIndicator1,
		'' AS MARCIndicator2, 
		m.SubFieldValue AS Title, 
		'' AS Section, 
		'' AS Volume,
		'' AS Heading,
		'' AS Publication,
		'' AS Relationship,
		1 AS Active
FROM	vwMarcDataField m
WHERE	m.DataFieldTag IN ('440', '490')
AND		m.Code = 'a'
AND		m.SubFieldValue <> ''
AND		m.MarcID = @MarcID

-- Add the section and volume information to the original data set.
-- Use generated sequence numbers to match the sections/volumes
-- with titles (there's no guaranteed relational way of making the
-- matches, so this is the best guess approach).
UPDATE	#tmpTitleAssociation
SET		Section = x.SubFieldValue
FROM	#tmpTitleAssociation t INNER JOIN (
				SELECT	ROW_NUMBER() OVER (PARTITION BY m.MarcDataFieldID
											ORDER BY m.MarcSubFieldID) AS NewSequence,
						m.*
				FROM	vwMarcDataField m
				WHERE	m.DataFieldTag IN ('440', '490')
				AND		m.Code = 'p'
				AND		m.SubFieldValue <> ''
				AND		m.MarcID = @MarcID
				) x
			ON t.MarcDataFieldID = x.MarcDataFieldID
			AND t.Sequence = x.NewSequence

UPDATE	#tmpTitleAssociation
SET		Volume = x.SubFieldValue
FROM	#tmpTitleAssociation t INNER JOIN (
				SELECT	ROW_NUMBER() OVER (PARTITION BY m.MarcDataFieldID
											ORDER BY m.MarcSubFieldID) AS NewSequence,
						m.*
				FROM	vwMarcDataField m
				WHERE	m.DataFieldTag IN ('440', '490')
				AND		m.Code = 'v'
				AND		m.SubFieldValue <> ''
				AND		m.MarcID = @MarcID
				) x
			ON t.MarcDataFieldID = x.MarcDataFieldID
			AND t.Sequence = x.NewSequence

-- Get the 830 records (these will be used to replace or augment certain 490 records)
SELECT	x.MarcDataFieldID, 
		x.DataFieldTag, 
		MIN([a]) AS Title, 
		MIN([p]) AS Section, 
		MIN([v]) AS Volume
INTO	#tmp830
FROM	(
		SELECT	MarcDataFieldID, DataFieldTag, Indicator1, [a], [p], [v]
		FROM	(SELECT * FROM dbo.vwMarcDataField
				WHERE DataFieldTag IN ('830')
				AND MarcID = @MarcID) AS m
		PIVOT	(MIN(SubFieldValue) FOR Code IN ([a], [p], [v])) AS Pvt
		) x
GROUP BY
		x.MarcDataFieldID, x.DataFieldTag

-- Delete the 490 records for which we have an 830 record, UNLESS there is
-- an identifier (code = x) associated with the 490.  The identifier gives
-- us a known, exact way to identify the series, and we don't want to throw
-- that away.
DELETE	#tmpTitleAssociation
WHERE	MARCTag = '490'
AND		MARCIndicator1 = '1'
AND		EXISTS(SELECT * FROM #tmp830)
AND		NOT EXISTS(	SELECT * FROM dbo.vwMarcDataField 
					WHERE DataFieldTag = '490' AND Code = 'x' AND MarcID = @MarcID)

-- Insert the 830 title associations when we haven't already collected a 490 record.
INSERT INTO #tmpTitleAssociation
SELECT DISTINCT
		0 AS Sequence,
		t8.MarcDatafieldID,
		t8.DataFieldTag,
		'',
		'',
		ISNULL(t8.Title, ''),
		ISNULL(t8.Section, ''),
		ISNULL(t8.Volume, ''),
		'',
		'',
		'',
		1
FROM	#tmp830 t8
WHERE	NOT EXISTS(SELECT * FROM #tmpTitleAssociation WHERE MARCTag = '490' AND MarcIndicator1 = '1')

DROP TABLE #tmp830

-- Get 780 and 785 tags with 't' or 'a' code (give preference to 't')
INSERT INTO #tmpTitleAssociation
SELECT DISTINCT
		0 AS Sequence, 
		m.MarcDataFieldID, 
		m.DataFieldTag, 
		'',
		m.Indicator2, 
		'' AS Title, 
		'' AS Section, 
		'' AS Volume,
		'' AS Heading,
		'' AS Publication,
		'' AS Relationship,
		1 AS Active
FROM	vwMarcDataField m
WHERE	m.DataFieldTag IN ('780', '785')
AND		m.Code IN ('t','a')
AND		m.SubFieldValue <> ''
AND		m.MarcID = @MarcID

UPDATE	#tmpTitleAssociation
SET		Title = m.SubFieldValue
FROM	#tmpTitleAssociation t INNER JOIN vwMarcDataField m
			ON t.MarcDataFieldID = m.MarcDataFieldID
WHERE	m.Code = 't'
AND		m.DataFieldTag IN ('780', '785')

UPDATE	#tmpTitleAssociation
SET		Title = CONVERT(NVARCHAR(200), m.SubFieldValue + ' ' + Title)
FROM	#tmpTitleAssociation t INNER JOIN vwMarcDataField m
			ON t.MarcDataFieldID = m.MarcDataFieldID
WHERE	m.Code = 'a'
AND		m.DataFieldTag IN ('780', '785')

-- Get 773 tags (this is Host Item information... defines "container" items for 
-- titles that are also articles)
INSERT INTO #tmpTitleAssociation
SELECT	0 AS Sequence,
		x.MarcDataFieldID,
		x.DataFieldTag,
		'',
		'',
		ISNULL(MIN([t]), '') AS Title,
		'' AS Section,
		'' AS Volume,
		ISNULL(MIN([a]), '') AS Heading, 
		ISNULL(MIN([d]), '') AS Publication, 
		ISNULL(MIN([g]), '') AS Relationship,
		1 AS Active
FROM	(
		SELECT	MarcDataFieldID, DataFieldTag, [a], [d], [g], [t]
		FROM	(SELECT * FROM dbo.vwMarcDataField
				WHERE DataFieldTag = '773'
				AND MarcID = @MarcID) AS m
		PIVOT	(MIN(SubFieldValue) FOR Code in ([a], [d], [g], [t])) AS Pvt
		) x
GROUP BY x.MarcDataFieldID, x.DataFieldTag

-- =======================================================================
-- Deliver the final result set
SELECT	Sequence,
		MARCDataFieldID,
		t.MARCTag,
		MARCIndicator1,
		t.MARCIndicator2,
		tat.TitleAssociationTypeID,
		Title,
		Section,
		Volume,
		Heading,
		Publication,
		Relationship,
		Active
FROM	#tmpTitleAssociation t INNER JOIN dbo.TitleAssociationType tat
			ON t.MARCTag = tat.MARCTag
			AND t.MARCIndicator2 = tat.MARCIndicator2

DROP TABLE #tmpTitleAssociation

SET NOCOUNT OFF

END

