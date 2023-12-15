CREATE PROCEDURE [dbo].[IAItemSelectAuto]

@ItemID INT

AS 

SET NOCOUNT ON

SELECT	
	[ItemID],
	[ItemStatusID],
	[IAIdentifierPrefix],
	[IAIdentifier],
	[Sponsor],
	[SponsorName],
	[ScanningCenter],
	[CallNumber],
	[ImageCount],
	[IdentifierAccessUrl],
	[Volume],
	[Note],
	[ScanOperator],
	[ScanDate],
	[ExternalStatus],
	[MARCBibID],
	[BarCode],
	[IADateStamp],
	[IAAddedDate],
	[LastOAIDataHarvestDate],
	[LastXMLDataHarvestDate],
	[LastProductionDate],
	[CreatedDate],
	[LastModifiedDate],
	[ShortTitle],
	[SponsorDate],
	[TitleID],
	[Year],
	[IdentifierBib],
	[ZQuery],
	[LicenseUrl],
	[Rights],
	[DueDiligence],
	[PossibleCopyrightStatus],
	[CopyrightRegion],
	[CopyrightComment],
	[CopyrightEvidence],
	[CopyrightEvidenceOperator],
	[CopyrightEvidenceDate],
	[LocalFileFolder],
	[NoMARCOk],
	[ScanningInstitution],
	[RightsHolder],
	[ItemDescription],
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
	[PageProgression],
	[CreatedUserID],
	[LastModifiedUserID]
FROM	
	[dbo].[IAItem]
WHERE	
	[ItemID] = @ItemID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.IAItemSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
