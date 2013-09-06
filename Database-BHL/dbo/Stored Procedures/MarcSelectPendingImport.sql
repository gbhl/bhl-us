CREATE PROCEDURE [dbo].[MarcSelectPendingImport]

@MarcImportBatchID INT

AS
BEGIN

SET NOCOUNT ON

SELECT	MarcID, 
		ISNULL([a], '') AS [TitlePart1],
		ISNULL([b], '') AS [TitlePart2],
		ISNULL([c], '') AS [Responsible], 
		ISNULL([n], '') AS [Number],
		ISNULL([p], '') AS [Part],
		TitleID AS [BHLTitleID],
		ShortTitle AS [BHLShortTitle]
FROM 
(
	SELECT	v.MarcID, 
			v.Code, 
			v.SubFieldValue,
			m.TitleID,
			t.ShortTitle
	FROM	dbo.vwMarcDataField v INNER JOIN dbo.Marc m
				ON v.MarcID = m.MarcID
			LEFT JOIN dbo.Title t
				ON m.TitleID = t.TitleID
	WHERE	DataFieldTag = '245'
	AND		Code in ('a', 'b', 'c', 'n', 'p')
	AND		m.MarcImportStatusID = 20
	AND		m.MarcImportBatchID = @MarcImportBatchID
) m
PIVOT
(
	MIN(SubFieldValue)
	FOR Code in ([a], [b], [c], [n], [p])
) pvt
ORDER BY [TitlePart1]
END
