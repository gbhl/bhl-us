SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [srchindex].[VolumeSelectDocumentsForIndex]

@TitleID int

AS 

BEGIN

SET NOCOUNT ON

CREATE TABLE #tmpVolumes
	(
	ItemSequence int NOT NULL,
	ItemID int NOT NULL,
	BookID int NOT NULL,
	Volume nvarchar(100) NOT NULL,
	[Year] nvarchar(20) NOT NULL,
	HasExternalContent int NOT NULL,
	HasLocalContent int NOT NULL,
	HasIllustrations int NOT NULL,
	HasSegments int NOT NULL
	)

INSERT	#tmpVolumes
SELECT  it.ItemSequence,
		it.ItemID,
		b.BookID,
        ISNULL(b.Volume, '') AS Volume,
		CASE 
			WHEN ISNULL(b.StartYear, '') = '' AND ISNULL(b.EndYear, '') = '' THEN ''
			WHEN ISNULL(b.StartYear, '') = '' THEN b.EndYear
			ELSE b.StartYear + CASE WHEN ISNULL(b.EndYear, '') = '' THEN '' ELSE '-' + b.EndYear END
		END AS [Year],
		CASE WHEN ISNULL(RTRIM(b.ExternalUrl), '') = '' THEN 0 ELSE 1 END AS HasExternalContent,
        0 AS HasLocalContent,
        0 AS HasIllustrations,
		0 AS HasSegments
FROM	dbo.ItemTitle it 
		INNER JOIN dbo.Item i ON it.ItemID = i.ItemID
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
WHERE	it.TitleID = @TitleID
AND		i.ItemStatusID = 40

UPDATE	#tmpVolumes
SET		HasSegments = 1
FROM	#tmpVolumes t 
		INNER JOIN dbo.ItemRelationship r ON t.ItemID = r.ParentID
		INNER JOIN dbo.Item si ON r.ChildID = si.ItemID AND si.ItemStatusID IN (30, 40)
		INNER JOIN dbo.Segment s ON si.ItemID = s.ItemID

UPDATE	#tmpVolumes
SET		HasLocalContent = 1
FROM	#tmpVolumes t INNER JOIN dbo.ItemPage ip ON t.ItemID = ip.ItemID

-- Return the final result set
SELECT	BookID AS ItemID,
		Volume,
		[Year],
		HasExternalContent,
		HasLocalContent,
		HasIllustrations,
		HasSegments
FROM	#tmpVolumes
ORDER BY ItemSequence

END


GO
