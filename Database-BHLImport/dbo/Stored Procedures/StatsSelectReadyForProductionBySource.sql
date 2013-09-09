
CREATE PROCEDURE [dbo].[StatsSelectReadyForProductionBySource]

@ImportSourceID INT

AS
BEGIN

SET NOCOUNT ON

-- Get the description of the "ready-to-be-published-to-production" status
DECLARE @Status nVARCHAR(20)
SELECT	@Status = Status FROM dbo.ImportStatus WHERE ImportStatusID = 10

SELECT	src.Source, @Status AS [Status], 'Title' AS [Type], SUM(CASE WHEN t.TitleID IS NULL THEN 0 ELSE 1 END) AS [Number Of Items]
FROM	dbo.ImportSource src LEFT JOIN dbo.Title t
			ON src.ImportSourceID = t.ImportSourceID
			AND t.ImportStatusID = 10
WHERE	src.ImportSourceID = @ImportSourceID
GROUP BY src.Source
UNION
SELECT	src.Source, @Status, 'Creator' AS [Type], SUM(CASE WHEN c.CreatorID IS NULL THEN 0 ELSE 1 END) AS [Number Of Items]
FROM	dbo.ImportSource src LEFT JOIN dbo.Creator c
			ON src.ImportSourceID = c.ImportSourceID
			AND c.ImportStatusID = 10
WHERE	src.ImportSourceID = @ImportSourceID
GROUP BY src.Source
UNION
SELECT	src.Source, @Status, 'Title_Creator' AS [Type], SUM(CASE WHEN c.TitleCreatorID IS NULL THEN 0 ELSE 1 END) AS [Number Of Items]
FROM	dbo.ImportSource src LEFT JOIN dbo.Title_Creator c
			ON src.ImportSourceID = c.ImportSourceID
			AND c.ImportStatusID = 10
WHERE	src.ImportSourceID = @ImportSourceID
GROUP BY src.Source
UNION
SELECT	src.Source, @Status, 'TitleTag' AS [Type], SUM(CASE WHEN t.TitleTagID IS NULL THEN 0 ELSE 1 END) AS [Number Of Items]
FROM	dbo.ImportSource src LEFT JOIN dbo.TitleTag t
			ON src.ImportSourceID = t.ImportSourceID
			AND t.ImportStatusID = 10
WHERE	src.ImportSourceID = @ImportSourceID
GROUP BY src.Source
UNION
SELECT	src.Source, @Status, 'Title_TitleIdentifier' AS [Type], SUM(CASE WHEN t.Title_TitleIdentifierID IS NULL THEN 0 ELSE 1 END) AS [Number Of Items]
FROM	dbo.ImportSource src LEFT JOIN dbo.Title_TitleIdentifier t
			ON src.ImportSourceID = t.ImportSourceID
			AND t.ImportStatusID = 10
WHERE	src.ImportSourceID = @ImportSourceID
GROUP BY src.Source
UNION 
SELECT	src.Source, @Status, 'TitleAssociation' AS [Type], SUM(CASE WHEN t.TitleAssociationID IS NULL THEN 0 ELSE 1 END) AS [Number Of Items]
FROM	dbo.ImportSource src LEFT JOIN dbo.TitleAssociation t
			ON src.ImportSourceID = t.ImportSourceID
			AND t.ImportStatusID = 10
WHERE	src.ImportSourceID = @ImportSourceID
GROUP BY src.Source
UNION 
SELECT	src.Source, @Status, 'TitleAssociation_TitleIdentifier' AS [Type], SUM(CASE WHEN t.TitleAssociation_TitleIdentifierID IS NULL THEN 0 ELSE 1 END) AS [Number Of Items]
FROM	dbo.ImportSource src LEFT JOIN dbo.TitleAssociation_TitleIdentifier t
			ON src.ImportSourceID = t.ImportSourceID
			AND t.ImportStatusID = 10
WHERE	src.ImportSourceID = @ImportSourceID
GROUP BY src.Source
UNION
SELECT	src.Source, @Status, 'Item' AS [Type], SUM(CASE WHEN i.ItemID IS NULL THEN 0 ELSE 1 END) AS [Number Of Items]
FROM	dbo.ImportSource src LEFT JOIN dbo.Item i
			ON src.ImportSourceID = i.ImportSourceID
			AND i.ImportStatusID = 10
WHERE	src.ImportSourceID = @ImportSourceID
GROUP BY src.Source
UNION
SELECT	src.Source, @Status, 'Page' AS [Type], SUM(CASE WHEN p.PageID IS NULL THEN 0 ELSE 1 END) AS [Number Of Items]
FROM	dbo.ImportSource src LEFT JOIN dbo.Page p
			ON src.ImportSourceID = p.ImportSourceID
			AND p.ImportStatusID = 10
WHERE	src.ImportSourceID = @ImportSourceID
GROUP BY src.Source
UNION
SELECT	src.Source, @Status, 'IndicatedPage' AS [Type], SUM(CASE WHEN ip.IndicatedPageID IS NULL THEN 0 ELSE 1 END) AS [Number Of Items]
FROM	dbo.ImportSource src LEFT JOIN dbo.IndicatedPage ip
			ON src.ImportSourceID = ip.ImportSourceID
			AND ip.ImportStatusID = 10
WHERE	src.ImportSourceID = @ImportSourceID
GROUP BY src.Source
UNION
SELECT	src.Source, @Status, 'Page_PageType' AS [Type], SUM(CASE WHEN ppt.PagePageTypeID IS NULL THEN 0 ELSE 1 END) AS [Number Of Items]
FROM	dbo.ImportSource src LEFT JOIN dbo.Page_PageType ppt
			ON src.ImportSourceID = ppt.ImportSourceID
			AND ppt.ImportStatusID = 10
WHERE	src.ImportSourceID = @ImportSourceID
GROUP BY src.Source
UNION
SELECT	src.Source, @Status, 'PageName' AS [Type], SUM(CASE WHEN pn.PageNameID IS NULL THEN 0 ELSE 1 END) AS [Number Of Items]
FROM	dbo.ImportSource src LEFT JOIN dbo.PageName pn
			ON src.ImportSourceID = pn.ImportSourceID
			AND pn.ImportStatusID = 10
WHERE	src.ImportSourceID = @ImportSourceID
GROUP BY src.Source
ORDER BY [Type]

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure StatsSelectReadyForProductionBySource. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

END
