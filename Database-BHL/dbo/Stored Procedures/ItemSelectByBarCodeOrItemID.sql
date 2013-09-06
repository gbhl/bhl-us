
CREATE PROCEDURE [dbo].[ItemSelectByBarCodeOrItemID]

@ItemID INT,
@BarCode nvarchar(40)

AS 

SET NOCOUNT ON

SELECT 
	[ItemID],
	I.[PrimaryTitleID],
	i.[RedirectItemID],
	i.[ThumbnailPageID],
	[BarCode],
	[MARCItemID],
	I.[CallNumber],
	[Volume],
	I.[InstitutionCode],
	I.[LanguageCode],
	[Sponsor],
	[ItemDescription],
	[ScannedBy],
	[PDFSize],
	i.[VaultID],
	[NumberOfFiles],
	I.[Note],
	[ItemStatusID],
	i.[ItemSourceID],
	ISNULL(s.[DownloadUrl] + i.BarCode, '') AS [DownloadUrl],
	[ScanningUser],
	[ScanningDate],
	[Year],
	[IdentifierBib],
	[FileRootFolder],
	[ZQuery],
	[LicenseUrl],
	[Rights],
	[DueDiligence],
	[CopyrightStatus],
	[CopyrightRegion],
	[CopyrightComment],
	[CopyrightEvidence],
	[CopyrightEvidenceOperator],
	[CopyrightEvidenceDate],
	[PaginationCompleteUserID],
	[PaginationCompleteDate],
	[PaginationStatusID],
	[PaginationStatusUserID],
	[PaginationStatusDate],
	[LastPageNameLookupDate],
	I.[CreationDate],
	I.[LastModifiedDate],
	I.[CreationUserID],
	I.[LastModifiedUserID],
	T.ShortTitle AS TitleName,
	I.ExternalUrl
FROM [dbo].[Item] I
	INNER JOIN dbo.Title T ON T.TitleID = I.PrimaryTitleID
	LEFT JOIN [dbo].[ItemSource] s ON i.[ItemSourceID] = s.[ItemSourceID]
	LEFT JOIN [dbo].[Vault] v ON i.[VaultID] = v.[VaultID]
WHERE
	[ItemID] = @ItemID OR
	Barcode = @Barcode



