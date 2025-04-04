CREATE PROCEDURE [dbo].[ItemSelectByTitleId]

@TitleId int

AS 

SET NOCOUNT ON;

WITH CTE (ItemID, FirstSegmentID)
AS
(
	SELECT	i.ItemID,
			MIN(s.SegmentID) AS FirstSegmentID
	FROM	[dbo].[Item] i 
			INNER JOIN [dbo].[ItemTitle] it ON i.ItemID = it.ItemID
			LEFT JOIN [dbo].[ItemRelationship] ir ON i.ItemID = ir.ParentID AND ir.SequenceOrder = 1
			LEFT JOIN [dbo].[Segment] s ON ir.ChildID = s.ItemID
	WHERE	it.TitleId = @TitleId 
	AND		i.ItemStatusID = 40
	GROUP BY i.ItemID
)
SELECT	b.BookID,
		b.ItemID,
		b.[BarCode],
		b.[Volume],
		b.[StartYear] AS [Year],
		b.[Sponsor],
		i.[ItemSourceID],
		CASE WHEN ISNULL(s.DownloadUrl, '') = '' THEN ''
			ELSE ISNULL(s.[DownloadUrl] + b.BarCode, '') END AS [DownloadUrl],
		b.[ScanningDate],
		ISNULL(i.ItemDescription, '') AS ItemDescription,
		ISNULL(b.LicenseUrl, '') AS LicenseUrl,
		ISNULL(b.ExternalUrl, '') AS ExternalUrl,
		ISNULL(b.Rights, '') AS Rights,
		ISNULL(b.DueDiligence, '') AS DueDiligence,
		ISNULL(b.CopyrightStatus, '') AS CopyrightStatus,
		'' AS CopyrightRegion,
		'' AS CopyrightComment,
		'' AS CopyrightEvidence,
		(SELECT COUNT(*) FROM dbo.SearchCatalogSegment WHERE ItemID = b.BookID) AS NumberOfSegments,
		b.IsVirtual,
		c.FirstPageID,
		cte.FirstSegmentID,
		seg.StartPageID AS FirstSegmentStartPageID,
		c.HasLocalContent
FROM	[dbo].[Item] i 
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
		LEFT JOIN [dbo].[ItemSource] s ON i.[ItemSourceID] = s.[ItemSourceID]
		LEFT JOIN [dbo].[Vault] v ON i.[VaultID] = v.[VaultID]
		INNER JOIN [dbo].[ItemTitle] it ON i.ItemID = it.ItemID
		INNER JOIN [dbo].[Title] t ON it.[TitleID] = t.[TitleID]
		LEFT JOIN [dbo].[SearchCatalog] c ON c.[TitleID] = t.[TitleID] AND c.[ItemID] = b.BookID
		INNER JOIN CTE cte ON i.ItemID = cte.ItemID
		--LEFT JOIN [dbo].[ItemRelationship] ir ON i.ItemID = ir.ParentID AND ir.SequenceOrder = 1
		--LEFT JOIN [dbo].[Segment] sg ON ir.ChildID = sg.ItemID
		LEFT JOIN dbo.Segment seg ON cte.FirstSegmentID = seg.SegmentID
WHERE	t.TitleId = @TitleId 
AND		i.ItemStatusID = 40
ORDER BY it.ItemSequence;

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemSelectByTitleId. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
