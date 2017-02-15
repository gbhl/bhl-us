CREATE PROCEDURE [dbo].[MarcSelectTitleKeywordsByMarcID]

@MarcID int

AS
BEGIN

SET NOCOUNT ON

-- =======================================================================
-- Build temp table

CREATE TABLE #tmpTitleKeyword
	(
	[Keyword] [nvarchar](200) NOT NULL,
	[MarcDataFieldTag] [nvarchar](50) NULL,
	[MarcSubFieldCode] [nvarchar](50) NULL
	)

-- =======================================================================
-- Populate the temp table

INSERT INTO #tmpTitleKeyword
SELECT	RTRIM(SUBSTRING(REPLACE(m.SubFieldValue, '[from old catalog]', ''), 1, 200)),
		m.DataFieldTag,
		m.Code
FROM	dbo.vwMarcDataField m
WHERE	m.DataFieldTag IN ('600', '610', '611', '630', '648', '650', '651', '652', 
							'653', '654', '655', '656', '657', '658', '662', '690')
AND		m.Indicator2 <> '6'	-- skip non-english-language subjects
AND		m.Code <> 'e' -- skip sources of tags
AND		m.Code <> '4' -- skip sources of tags
AND		m.Code <> '3' -- skip sources of tags
AND		m.Code <> '2' -- skip sources of tags
AND		m.Code <> '0' -- skip authority record control numbers
AND		m.MarcID = @MarcID

-- =======================================================================
-- Deliver the final result set

-- Strip trailing periods from tags
UPDATE	#tmpTitleKeyword
SET		Keyword = CASE WHEN RIGHT(Keyword, 1) = '.'
					THEN LEFT(Keyword, LEN(Keyword) - 1)
					ELSE Keyword
					END

SELECT	SUBSTRING(Keyword, 1, 50) AS Keyword,
		MIN(MarcDataFieldTag) AS MarcDataFieldTag,
		MIN(MarcSubFieldCode) AS MarcSubFieldCode
FROM	#tmpTitleKeyword
GROUP BY
		SUBSTRING(Keyword, 1, 50)

DROP TABLE #tmpTitleKeyword

END
