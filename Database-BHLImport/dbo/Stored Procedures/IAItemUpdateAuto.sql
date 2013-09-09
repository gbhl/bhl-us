
-- IAItemUpdateAuto PROCEDURE
-- Generated 10/14/2011 12:13:11 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for IAItem

CREATE PROCEDURE IAItemUpdateAuto

@ItemID INT,
@ItemStatusID INT,
@LocalFileFolder NVARCHAR(200),
@IAIdentifierPrefix NVARCHAR(50),
@IAIdentifier NVARCHAR(50),
@Sponsor NVARCHAR(100),
@SponsorName NVARCHAR(50),
@SponsorDate NVARCHAR(50),
@ScanningCenter NVARCHAR(50),
@CallNumber NVARCHAR(50),
@ImageCount INT,
@IdentifierAccessUrl NVARCHAR(100),
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
@ShortTitle NVARCHAR(255),
@BarCode NVARCHAR(40),
@IADateStamp DATETIME,
@IAAddedDate DATETIME,
@LastOAIDataHarvestDate DATETIME,
@LastXMLDataHarvestDate DATETIME,
@LastProductionDate DATETIME,
@NoMARCOk TINYINT

AS 

SET NOCOUNT ON

UPDATE [dbo].[IAItem]

SET

	[ItemStatusID] = @ItemStatusID,
	[LocalFileFolder] = @LocalFileFolder,
	[IAIdentifierPrefix] = @IAIdentifierPrefix,
	[IAIdentifier] = @IAIdentifier,
	[Sponsor] = @Sponsor,
	[SponsorName] = @SponsorName,
	[SponsorDate] = @SponsorDate,
	[ScanningCenter] = @ScanningCenter,
	[CallNumber] = @CallNumber,
	[ImageCount] = @ImageCount,
	[IdentifierAccessUrl] = @IdentifierAccessUrl,
	[Volume] = @Volume,
	[Note] = @Note,
	[ScanOperator] = @ScanOperator,
	[ScanDate] = @ScanDate,
	[ExternalStatus] = @ExternalStatus,
	[TitleID] = @TitleID,
	[Year] = @Year,
	[IdentifierBib] = @IdentifierBib,
	[ZQuery] = @ZQuery,
	[MARCBibID] = @MARCBibID,
	[LicenseUrl] = @LicenseUrl,
	[Rights] = @Rights,
	[DueDiligence] = @DueDiligence,
	[PossibleCopyrightStatus] = @PossibleCopyrightStatus,
	[CopyrightRegion] = @CopyrightRegion,
	[CopyrightComment] = @CopyrightComment,
	[CopyrightEvidence] = @CopyrightEvidence,
	[CopyrightEvidenceOperator] = @CopyrightEvidenceOperator,
	[CopyrightEvidenceDate] = @CopyrightEvidenceDate,
	[ShortTitle] = @ShortTitle,
	[BarCode] = @BarCode,
	[IADateStamp] = @IADateStamp,
	[IAAddedDate] = @IAAddedDate,
	[LastOAIDataHarvestDate] = @LastOAIDataHarvestDate,
	[LastXMLDataHarvestDate] = @LastXMLDataHarvestDate,
	[LastProductionDate] = @LastProductionDate,
	[NoMARCOk] = @NoMARCOk,
	[LastModifiedDate] = getdate()

WHERE
	[ItemID] = @ItemID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAItemUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

