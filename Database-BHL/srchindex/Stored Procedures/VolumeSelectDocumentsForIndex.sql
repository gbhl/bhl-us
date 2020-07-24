CREATE PROCEDURE [srchindex].[VolumeSelectDocumentsForIndex]

@TitleID int

AS 

BEGIN

SET NOCOUNT ON

CREATE TABLE #tmpVolumes
	(
	ItemSequence int NOT NULL,
	ItemID int NOT NULL,
	Volume nvarchar(100) NOT NULL,
	[Year] nvarchar(20) NOT NULL,
	HasExternalContent int NOT NULL,
	HasLocalContent int NOT NULL,
	HasIllustrations int NOT NULL,
	HasSegments int NOT NULL
	)

INSERT	#tmpVolumes
SELECT  ti.ItemSequence,
		i.ItemID,
        ISNULL(i.Volume, '') AS Volume,
		CASE 
			WHEN ISNULL(i.Year, '') = '' AND ISNULL(i.EndYear, '') = '' THEN ''
			WHEN ISNULL(i.Year, '') = '' THEN i.EndYear
			ELSE i.Year + CASE WHEN ISNULL(i.EndYear, '') = '' THEN '' ELSE '-' + i.EndYear END
		END AS [Year],
		CASE WHEN ISNULL(RTRIM(i.ExternalUrl), '') = '' THEN 0 ELSE 1 END AS HasExternalContent,
        0 AS HasLocalContent,
        0 AS HasIllustrations,
		0 AS HasSegments
FROM	dbo.TitleItem ti INNER JOIN dbo.Item i ON ti.ItemID = i.ItemID
WHERE	ti.TitleID = @TitleID
AND		i.ItemStatusID = 40

UPDATE	#tmpVolumes
SET		HasSegments = 1
FROM	#tmpVolumes t INNER JOIN dbo.Segment s ON t.ItemID = s.ItemID AND s.SegmentStatusID IN (10, 20)

UPDATE	#tmpVolumes
SET		HasLocalContent = 1
FROM	#tmpVolumes t INNER JOIN dbo.Page p ON t.ItemID = p.ItemID

-- Return the final result set
SELECT	ItemID,
		Volume,
		[Year],
		HasExternalContent,
		HasLocalContent,
		HasIllustrations,
		HasSegments
FROM	#tmpVolumes
ORDER BY ItemSequence

END
