CREATE PROCEDURE [dbo].[ItemSelectByBarCodeOrItemID]

@ItemID INT,
@BarCode nvarchar(40)

AS 

SET NOCOUNT ON

SELECT	[ItemID],
		[PrimaryTitleID],
		[BarCode],
		[MARCItemID],
		i.[CallNumber],
		[Volume],
		i.[LanguageCode],
		[ItemDescription],
		[ScannedBy],
		[PDFSize],
		i.[VaultID],
		i.[Note],
		i.[CreationDate],
		i.[LastModifiedDate],
		i.[CreationUserID],
		i.[LastModifiedUserID],
		[ItemStatusID],
		[ScanningUser],
		[ScanningDate],
		[PaginationCompleteUserID],
		[PaginationCompleteDate],
		[PaginationStatusID],
		[PaginationStatusUserID],
		[PaginationStatusDate],
		[LastPageNameLookupDate],
		i.[ItemSourceID],
		[Year],
		[IdentifierBib],
		[FileRootFolder],
		[ZQuery],
		[Sponsor],
		[LicenseUrl],
		[Rights],
		[DueDiligence],
		[CopyrightStatus],
		[CopyrightRegion],
		[CopyrightComment],
		[CopyrightEvidence],
		[CopyrightEvidenceOperator],
		[CopyrightEvidenceDate],
		i.[ThumbnailPageID],
		i.[RedirectItemID],
		i.ExternalUrl,
		i.[EndYear],
		[StartVolume],
		[EndVolume],
		[StartIssue],
		[EndIssue],
		[StartNumber],
		[EndNumber],
		[StartSeries],
		[EndSeries],
		[StartPart],
		[EndPart],
		[VolumeReviewed],
		[VolumeReviewedDate],
		[VolumeReviewedUserID],
		ISNULL(s.[DownloadUrl] + i.BarCode, '') AS [DownloadUrl],
		t.ShortTitle AS TitleName
FROM	[dbo].[Item] i
		INNER JOIN dbo.Title t ON t.TitleID = i.PrimaryTitleID
		LEFT JOIN [dbo].[ItemSource] s ON i.[ItemSourceID] = s.[ItemSourceID]
		LEFT JOIN [dbo].[Vault] v ON i.[VaultID] = v.[VaultID]
WHERE	[ItemID] = @ItemID OR
		Barcode = @Barcode
