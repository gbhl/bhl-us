CREATE PROCEDURE [dbo].[ItemSelectByBarCodeOrItemID]

@ItemID INT,
@BarCode nvarchar(200)

AS 

SET NOCOUNT ON

-- If both @ItemID and @Barcode are specified, @ItemID takes precedence
IF (@ItemID > 0) SELECT @BarCode = BarCode FROM dbo.Book WHERE BookID = @ItemID

SELECT	b.BookID,
		b.ItemID,
		t.TitleID AS PrimaryTitleID,
		b.[BarCode],
		b.MARCItemID,
		b.[CallNumber],
		b.[Volume],
		b.[LanguageCode],
		i.[ItemDescription],
		CAST(NULL AS int) AS ScannedBy,
		CAST(NULL AS int) AS PDFSize,
		i.[VaultID],
		i.[Note],
		b.[CreationDate],
		b.[LastModifiedDate],
		b.[CreationUserID],
		b.[LastModifiedUserID],
		i.[ItemStatusID],
		b.ScanningUser,
		b.ScanningDate,
		b.[PaginationCompleteUserID],
		b.[PaginationCompleteDate],
		b.[PaginationStatusID],
		b.[PaginationStatusUserID],
		b.[PaginationStatusDate],
		b.[LastPageNameLookupDate],
		i.[ItemSourceID],
		s.[SourceName],
		b.[StartYear],
		b.[IdentifierBib],
		i.[FileRootFolder],
		b.[ZQuery],
		b.[Sponsor],
		b.[LicenseUrl],
		b.[Rights],
		b.[DueDiligence],
		b.[CopyrightIndicator],
		b.[CopyrightStatus],
		CAST(NULL AS nvarchar(50)) AS CopyrightRegion,
		CAST(NULL AS nvarchar(max)) AS CopyrightComment,
		CAST(NULL AS nvarchar(max)) CopyrightEvidence,
		CAST(NULL AS nvarchar(100)) CopyrightEvidenceOperator,
		CAST(NULL AS nvarchar(30)) CopyrightEvidenceDate,
		b.[ThumbnailPageID],
		b.RedirectBookID,
		b.ExternalUrl,
		b.[EndYear],
		b.[StartVolume],
		b.[EndVolume],
		b.[StartIssue],
		b.[EndIssue],
		b.[StartNumber],
		b.[EndNumber],
		b.[StartSeries],
		b.[EndSeries],
		b.[StartPart],
		b.[EndPart],
		b.[PageProgression],
		b.[IsVirtual],
		b.[VirtualVolumeKey],
		CAST(NULL AS tinyint) AS [VolumeReviewed],
		CAST(NULL AS datetime) AS [VolumeReviewedDate],
		CAST(NULL AS int) AS [VolumeReviewedUserID],
		ISNULL(s.[DownloadUrl] + b.BarCode, '') AS [DownloadUrl],
		t.ShortTitle AS TitleName
FROM	[dbo].[Item] i
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
		INNER JOIN dbo.ItemTitle it ON i.ItemID = it.ItemID AND it.IsPrimary = 1
		INNER JOIN dbo.Title t ON it.TitleID = t.TitleID
		LEFT JOIN [dbo].[ItemSource] s ON i.[ItemSourceID] = s.[ItemSourceID]
		LEFT JOIN [dbo].[Vault] v ON i.[VaultID] = v.[VaultID]
WHERE	b.BookID = @ItemID OR
		b.Barcode = @Barcode

GO
