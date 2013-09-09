
CREATE PROCEDURE [dbo].[IAItemSelectByIAIdentifier]

@IAIdentifier NVARCHAR(50)

AS
SET NOCOUNT ON

SELECT 

	[ItemID],
	[ItemStatusID],
	[LocalFileFolder],
	[IAIdentifierPrefix],
	[IAIdentifier],
	[Sponsor],
	[SponsorName],
	[SponsorDate],
	[ScanningCenter],
	[CallNumber],
	[ImageCount],
	[IdentifierAccessUrl],
	[Volume],
	[Note],
	[ScanOperator],
	[ScanDate],
	[ExternalStatus],
	[TitleID],
	[Year],
	[IdentifierBib],
	[ZQuery],
	[MARCBibID],
	[LicenseUrl],
	[Rights],
	[DueDiligence],
	[PossibleCopyrightStatus],
	[CopyrightRegion],
	[CopyrightComment],
	[CopyrightEvidence],
	[CopyrightEvidenceOperator],
	[CopyrightEvidenceDate],
	[ShortTitle],
	[BarCode],
	[IADateStamp],
	[IAAddedDate],
	[LastOAIDataHarvestDate],
	[LastXMLDataHarvestDate],
	[LastProductionDate],
	[NoMARCOk],
	[CreatedDate],
	[LastModifiedDate]

FROM [dbo].[IAItem]

WHERE
	[IAIdentifier] = @IAIdentifier

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAItemSelectByIAIdentifier. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


