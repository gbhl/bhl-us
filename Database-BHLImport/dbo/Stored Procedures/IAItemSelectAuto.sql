
-- IAItemSelectAuto PROCEDURE
-- Generated 10/14/2011 12:13:11 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for IAItem

CREATE PROCEDURE IAItemSelectAuto

@ItemID INT

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
	[ItemID] = @ItemID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAItemSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

