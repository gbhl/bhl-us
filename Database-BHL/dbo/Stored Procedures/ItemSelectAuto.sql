CREATE PROCEDURE [dbo].[ItemSelectAuto]

@ItemID INT

AS 

SET NOCOUNT ON

SELECT	
	[ItemID],
	[PrimaryTitleID],
	[BarCode],
	[MARCItemID],
	[CallNumber],
	[Volume],
	[InstitutionCode],
	[LanguageCode],
	[ItemDescription],
	[ScannedBy],
	[PDFSize],
	[VaultID],
	[NumberOfFiles],
	[Note],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID],
	[ItemStatusID],
	[ScanningUser],
	[ScanningDate],
	[PaginationCompleteUserID],
	[PaginationCompleteDate],
	[PaginationStatusID],
	[PaginationStatusUserID],
	[PaginationStatusDate],
	[LastPageNameLookupDate],
	[ItemSourceID],
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
	[ThumbnailPageID],
	[RedirectItemID],
	[ExternalUrl],
	[EndYear],
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
	[VolumeReviewedUserID]
FROM	
	[dbo].[Item]
WHERE	
	[ItemID] = @ItemID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.ItemSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
