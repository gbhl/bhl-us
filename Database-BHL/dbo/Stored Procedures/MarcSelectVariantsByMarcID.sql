CREATE PROCEDURE [dbo].[MarcSelectVariantsByMarcID]

@MarcID int

AS
BEGIN

SET NOCOUNT ON

-- =======================================================================
-- Build temp table

CREATE TABLE #tmpTitleVariant(
	[MARCDataFieldID] [int] NOT NULL,
	[MARCTag] [nvarchar](20) NOT NULL,
	[MARCIndicator2] [nchar](1) NOT NULL DEFAULT (''),
	[Title] [nvarchar](MAX) NOT NULL DEFAULT (''),
	[TitleRemainder] [nvarchar](MAX) NOT NULL DEFAULT(''),
	[PartNumber] [nvarchar](255) NOT NULL DEFAULT(''),
	[PartName] [nvarchar](255) NOT NULL DEFAULT('')
	)

-- =======================================================================
-- Populate the temp table

-- Get 210, 242, and 246 tags with an 'a' code
INSERT INTO #tmpTitleVariant
SELECT	m.MarcDataFieldID,
		m.DataFieldTag, 
		CASE WHEN m.DataFieldTag = '246' AND m.Indicator2 = '1' THEN '1' ELSE '' END AS MARCIndicator2,
		dbo.fnRemoveTrailingPunctuation(m.SubFieldValue, DEFAULT) AS Title, 
		'' AS TitleRemainder, 
		'' AS PartNumber,
		'' AS PartName
FROM	vwMarcDataField m
WHERE	m.DataFieldTag IN ('210', '242', '246')
AND		m.Code = 'a'
AND		m.SubFieldValue <> ''
AND		m.MarcID = @MarcID

-- Add the title remainders to the original data set.
UPDATE	#tmpTitleVariant
-- As this field may contain date range values (ex. "1990-"), don't remove trailing hyphens when cleaning punctuation
SET		TitleRemainder = dbo.fnRemoveTrailingPunctuation(x.SubFieldValue, '[a-zA-Z0-9)\]?!>*%"''-]%')
FROM	#tmpTitleVariant t INNER JOIN (
				SELECT	m.*
				FROM	vwMarcDataField m
				WHERE	m.DataFieldTag IN ('210', '242', '246')
				AND		m.Code = 'b'
				AND		m.SubFieldValue <> ''
				AND		m.MarcID = @MarcID
				) x
			ON t.MarcDataFieldID = x.MarcDataFieldID

-- Add the part numbers to the original data set.
UPDATE	#tmpTitleVariant
SET		PartNumber = dbo.fnRemoveTrailingPunctuation(SUBSTRING(x.SubFieldValue, 1, 255), DEFAULT)
FROM	#tmpTitleVariant t INNER JOIN (
				SELECT	m.*
				FROM	vwMarcDataField m
				WHERE	m.DataFieldTag IN ('242', '246')
				AND		m.Code = 'n'
				AND		m.SubFieldValue <> ''
				AND		m.MarcID = @MarcID
				) x
			ON t.MarcDataFieldID = x.MarcDataFieldID

-- Add the part names to the original data set.
UPDATE	#tmpTitleVariant
SET		PartName = dbo.fnRemoveTrailingPunctuation(SUBSTRING(x.SubFieldValue, 1, 255), DEFAULT)
FROM	#tmpTitleVariant t INNER JOIN (
				SELECT	m.*
				FROM	vwMarcDataField m
				WHERE	m.DataFieldTag IN ('242', '246')
				AND		m.Code = 'p'
				AND		m.SubFieldValue <> ''
				AND		m.MarcID = @MarcID
				) x
			ON t.MarcDataFieldID = x.MarcDataFieldID

-- =======================================================================
-- Deliver the final result set
SELECT	MARCDataFieldID,
		t.MARCTag,
		t.MARCIndicator2,
		tvt.TitleVariantTypeID,
		RTRIM(REPLACE(Title, '[from old catalog]', '')) AS Title,
		TitleRemainder,
		PartNumber,
		PartName
FROM	#tmpTitleVariant t INNER JOIN dbo.TitleVariantType tvt
			ON t.MARCTag = tvt.MARCTag
			AND t.MARCIndicator2 = tvt.MARCIndicator2

DROP TABLE #tmpTitleVariant

END

GO
