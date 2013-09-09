
-- IAItemInsertAuto PROCEDURE
-- Generated 10/14/2011 12:13:11 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for IAItem

CREATE PROCEDURE IAItemInsertAuto

@ItemID INT OUTPUT,
@ItemStatusID INT,
@LocalFileFolder NVARCHAR(200),
@IAIdentifierPrefix NVARCHAR(50),
@IAIdentifier NVARCHAR(50),
@Sponsor NVARCHAR(100),
@SponsorName NVARCHAR(50) = null,
@SponsorDate NVARCHAR(50) = null,
@ScanningCenter NVARCHAR(50),
@CallNumber NVARCHAR(50),
@ImageCount INT = null,
@IdentifierAccessUrl NVARCHAR(100) = null,
@Volume NVARCHAR(50),
@Note NVARCHAR(255),
@ScanOperator NVARCHAR(100),
@ScanDate NVARCHAR(50),
@ExternalStatus NVARCHAR(50),
@TitleID NVARCHAR(50),
@Year NVARCHAR(20),
@IdentifierBib NVARCHAR(50),
@ZQuery NVARCHAR(200),
@MARCBibID NVARCHAR(50),
@LicenseUrl NVARCHAR(MAX),
@Rights NVARCHAR(MAX),
@DueDiligence NVARCHAR(MAX),
@PossibleCopyrightStatus NVARCHAR(MAX),
@CopyrightRegion NVARCHAR(50),
@CopyrightComment NVARCHAR(MAX),
@CopyrightEvidence NVARCHAR(MAX),
@CopyrightEvidenceOperator NVARCHAR(100),
@CopyrightEvidenceDate NVARCHAR(30),
@ShortTitle NVARCHAR(255) = null,
@BarCode NVARCHAR(40),
@IADateStamp DATETIME = null,
@IAAddedDate DATETIME = null,
@LastOAIDataHarvestDate DATETIME = null,
@LastXMLDataHarvestDate DATETIME = null,
@LastProductionDate DATETIME = null,
@NoMARCOk TINYINT

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[IAItem]
(
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
)
VALUES
(
	@ItemStatusID,
	@LocalFileFolder,
	@IAIdentifierPrefix,
	@IAIdentifier,
	@Sponsor,
	@SponsorName,
	@SponsorDate,
	@ScanningCenter,
	@CallNumber,
	@ImageCount,
	@IdentifierAccessUrl,
	@Volume,
	@Note,
	@ScanOperator,
	@ScanDate,
	@ExternalStatus,
	@TitleID,
	@Year,
	@IdentifierBib,
	@ZQuery,
	@MARCBibID,
	@LicenseUrl,
	@Rights,
	@DueDiligence,
	@PossibleCopyrightStatus,
	@CopyrightRegion,
	@CopyrightComment,
	@CopyrightEvidence,
	@CopyrightEvidenceOperator,
	@CopyrightEvidenceDate,
	@ShortTitle,
	@BarCode,
	@IADateStamp,
	@IAAddedDate,
	@LastOAIDataHarvestDate,
	@LastXMLDataHarvestDate,
	@LastProductionDate,
	@NoMARCOk,
	getdate(),
	getdate()
)

SET @ItemID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAItemInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	
	RETURN -- insert successful
END

