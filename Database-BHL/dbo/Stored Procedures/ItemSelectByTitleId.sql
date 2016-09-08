CREATE PROCEDURE [dbo].[ItemSelectByTitleId]

@TitleId int

AS 

SET NOCOUNT ON

SELECT	i.[ItemID],
		i.[BarCode],
		i.[Volume],
		i.[Sponsor],
		i.[ItemSourceID],
		CASE WHEN ISNULL(s.DownloadUrl, '') = '' THEN ''
			ELSE ISNULL(s.[DownloadUrl] + i.BarCode, '') END AS [DownloadUrl],
		i.[ScanningDate],
		ISNULL(i.ItemDescription, '') AS ItemDescription,
		ISNULL(i.LicenseUrl, '') AS LicenseUrl,
		ISNULL(i.ExternalUrl, '') AS ExternalUrl,
		ISNULL(i.Rights, '') AS Rights,
		ISNULL(i.DueDiligence, '') AS DueDiligence,
		ISNULL(i.CopyrightStatus, '') AS CopyrightStatus,
		ISNULL(i.CopyrightRegion, '') AS CopyrightRegion,
		ISNULL(i.CopyrightComment, '') AS CopyrightComment,
		ISNULL(i.CopyrightEvidence, '') AS CopyrightEvidence,
		(SELECT COUNT(*) FROM dbo.Segment WHERE ItemID = i.ItemID AND SegmentStatusID IN (10,20)) AS NumberOfSegments,
		c.FirstPageID
FROM	[dbo].[Item] i 
		LEFT JOIN [dbo].[ItemSource] s ON i.[ItemSourceID] = s.[ItemSourceID]
		LEFT JOIN [dbo].[Vault] v ON i.[VaultID] = v.[VaultID]
		INNER JOIN [dbo].[TitleItem] ti ON i.ItemID = ti.ItemID
		INNER JOIN [dbo].[Title] t ON ti.[TitleID] = t.[TitleID]
		LEFT JOIN [dbo].[SearchCatalog] c ON c.[TitleID] = t.[TitleID] AND c.[ItemID] = i.[ItemID]
WHERE	t.TitleId = @TitleId 
AND		i.ItemStatusID = 40
ORDER BY ti.ItemSequence

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemSelectByTitleId. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
