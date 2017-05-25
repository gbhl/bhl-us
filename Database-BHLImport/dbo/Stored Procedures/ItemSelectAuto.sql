CREATE PROCEDURE [dbo].[ItemSelectAuto]

@ItemID INT

AS 

SET NOCOUNT ON

SELECT	
	[ItemID],
	[ImportKey],
	[ImportStatusID],
	[ImportSourceID],
	[MARCBibID],
	[BarCode],
	[ItemSequence],
	[MARCItemID],
	[CallNumber],
	[Volume],
	[InstitutionCode],
	[LanguageCode],
	[Sponsor],
	[ItemDescription],
	[ScannedBy],
	[PDFSize],
	[VaultID],
	[NumberOfFiles],
	[Note],
	[ItemStatusID],
	[ScanningUser],
	[ScanningDate],
	[PaginationCompleteUserID],
	[PaginationCompleteDate],
	[PaginationStatusID],
	[PaginationStatusUserID],
	[PaginationStatusDate],
	[LastPageNameLookupDate],
	[ExternalCreationDate],
	[ExternalLastModifiedDate],
	[ExternalCreationUser],
	[ExternalLastModifiedUser],
	[ProductionDate],
	[CreatedDate],
	[LastModifiedDate],
	[Year],
	[IdentifierBib],
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
	[ScanningInstitutionCode],
	[RightsHolderCode],
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
	[EndPart]
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
