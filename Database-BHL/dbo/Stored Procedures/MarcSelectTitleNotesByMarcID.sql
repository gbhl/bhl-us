CREATE PROCEDURE [dbo].[MarcSelectTitleNotesByMarcID]

@MarcID int

AS
BEGIN

SET NOCOUNT ON

-- =======================================================================
-- Build temp table

CREATE TABLE #tmpTitleNote
	(
	[NoteText] [nvarchar](MAX) NOT NULL,
	[MarcDataFieldTag] [nvarchar](5) NULL,
	[MarcIndicator1] [nvarchar](5) NULL,
	[NoteSequence] [smallint] NULL
	);

-- =======================================================================
-- Populate the temp table

WITH basetable AS
(
	-- Select all note fields, excluding those with a code '5'
	SELECT	ROW_NUMBER() OVER (PARTITION BY MarcDataFieldID ORDER BY MarcSubFieldID) AS RowNum,
			COUNT(*) OVER (PARTITION BY MarcDataFieldID) NumRows,
			MarcID, MarcDataFieldID, MarcSubFieldID, DataFieldTag, Indicator1, Code, 
			CAST(SubFieldValue AS NVARCHAR(MAX)) SubFieldValue
	FROM	dbo.vwMarcDataField
	WHERE	MarcID = @MarcID
	AND		DataFieldTag IN ('500','502','505','510','515','520','525','545','546','547','550','580')
	AND		SubFieldValue <> ''
	AND		MarcDataFieldID NOT IN (
				SELECT	DISTINCT MarcDataFieldID
				FROM	dbo.vwMarcDataField
				WHERE	MarcID = @MarcID
				AND		DataFieldTag IN ('500','502','505','510','515','520','525','545','546','547','550','580')
				AND		Code = '5'
				)
),
rCTE AS (
	SELECT	RowNum, NumRows, MarcID, MarcDataFieldID, MarcSubFieldID, DataFieldTag, Indicator1, SubFieldValue
	FROM	basetable
	WHERE	RowNum = 1
	UNION ALL
	SELECT	r.RowNum + 1, b.NumRows, r.MarcID, r.MarcDataFieldID, r.MarcSubFieldID, r.DataFieldTag, 
			r.Indicator1, r.SubFieldValue + ' | ' + b.SubFieldValue AS SubFieldValue
	FROM	basetable b 
			INNER JOIN rCTE r ON b.MarcID = r.MarcID
			AND b.MarcDataFieldID = r.MarcDataFieldID
			AND b.RowNum = r.RowNum + 1
)
INSERT #tmpTitleNote
SELECT	SubFieldValue AS NoteText, DataFieldTag, Indicator1, 
		ROW_NUMBER() OVER (PARTITION BY MarcID ORDER BY MarcSubFieldID) AS NoteSequence
FROM	rCTE
WHERE	NumRows = RowNum
ORDER BY MarcDataFieldID, RowNum

-- =======================================================================
-- Deliver the final result set

SELECT	nt.NoteTypeID,
		LTRIM(RTRIM(t.NoteText)) AS NoteText,
		t.NoteSequence
FROM	#tmpTitleNote t INNER JOIN dbo.NoteType nt
			ON t.MarcDataFieldTag = nt.MarcDataFieldTag
			AND t.MarcIndicator1 = nt.MarcIndicator1
ORDER BY t.NoteSequence

DROP TABLE #tmpTitleNote

END
