
-- ItemUpdateAuto PROCEDURE
-- Generated 12/18/2008 2:14:22 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for Item

CREATE PROCEDURE ItemUpdateAuto

@ItemID INT,
@ImportKey NVARCHAR(50),
@ImportStatusID INT,
@ImportSourceID INT,
@MARCBibID NVARCHAR(50),
@BarCode NVARCHAR(40),
@ItemSequence SMALLINT,
@MARCItemID NVARCHAR(50),
@CallNumber NVARCHAR(100),
@Volume NVARCHAR(100),
@InstitutionCode NVARCHAR(10),
@LanguageCode NVARCHAR(10),
@Sponsor NVARCHAR(100),
@ItemDescription NTEXT,
@ScannedBy INT,
@PDFSize INT,
@VaultID INT,
@NumberOfFiles SMALLINT,
@Note NVARCHAR(255),
@ItemStatusID INT,
@ScanningUser NVARCHAR(100),
@ScanningDate DATETIME,
@Year NVARCHAR(20),
@IdentifierBib NVARCHAR(50),
@ZQuery NVARCHAR(200),
@LicenseUrl NVARCHAR(MAX),
@Rights NVARCHAR(MAX),
@DueDiligence NVARCHAR(MAX),
@CopyrightStatus NVARCHAR(MAX),
@CopyrightRegion NVARCHAR(50),
@CopyrightComment NVARCHAR(MAX),
@CopyrightEvidence NVARCHAR(MAX),
@CopyrightEvidenceOperator NVARCHAR(100),
@CopyrightEvidenceDate NVARCHAR(30),
@PaginationCompleteUserID INT,
@PaginationCompleteDate DATETIME,
@PaginationStatusID INT,
@PaginationStatusUserID INT,
@PaginationStatusDate DATETIME,
@LastPageNameLookupDate DATETIME,
@ExternalCreationDate DATETIME,
@ExternalLastModifiedDate DATETIME,
@ExternalCreationUser INT,
@ExternalLastModifiedUser INT,
@ProductionDate DATETIME

AS 

SET NOCOUNT ON

UPDATE [dbo].[Item]

SET

	[ImportKey] = @ImportKey,
	[ImportStatusID] = @ImportStatusID,
	[ImportSourceID] = @ImportSourceID,
	[MARCBibID] = @MARCBibID,
	[BarCode] = @BarCode,
	[ItemSequence] = @ItemSequence,
	[MARCItemID] = @MARCItemID,
	[CallNumber] = @CallNumber,
	[Volume] = @Volume,
	[InstitutionCode] = @InstitutionCode,
	[LanguageCode] = @LanguageCode,
	[Sponsor] = @Sponsor,
	[ItemDescription] = @ItemDescription,
	[ScannedBy] = @ScannedBy,
	[PDFSize] = @PDFSize,
	[VaultID] = @VaultID,
	[NumberOfFiles] = @NumberOfFiles,
	[Note] = @Note,
	[ItemStatusID] = @ItemStatusID,
	[ScanningUser] = @ScanningUser,
	[ScanningDate] = @ScanningDate,
	[Year] = @Year,
	[IdentifierBib] = @IdentifierBib,
	[ZQuery] = @ZQuery,
	[LicenseUrl] = @LicenseUrl,
	[Rights] = @Rights,
	[DueDiligence] = @DueDiligence,
	[CopyrightStatus] = @CopyrightStatus,
	[CopyrightRegion] = @CopyrightRegion,
	[CopyrightComment] = @CopyrightComment,
	[CopyrightEvidence] = @CopyrightEvidence,
	[CopyrightEvidenceOperator] = @CopyrightEvidenceOperator,
	[CopyrightEvidenceDate] = @CopyrightEvidenceDate,
	[PaginationCompleteUserID] = @PaginationCompleteUserID,
	[PaginationCompleteDate] = @PaginationCompleteDate,
	[PaginationStatusID] = @PaginationStatusID,
	[PaginationStatusUserID] = @PaginationStatusUserID,
	[PaginationStatusDate] = @PaginationStatusDate,
	[LastPageNameLookupDate] = @LastPageNameLookupDate,
	[ExternalCreationDate] = @ExternalCreationDate,
	[ExternalLastModifiedDate] = @ExternalLastModifiedDate,
	[ExternalCreationUser] = @ExternalCreationUser,
	[ExternalLastModifiedUser] = @ExternalLastModifiedUser,
	[ProductionDate] = @ProductionDate,
	[LastModifiedDate] = getdate()

WHERE
	[ItemID] = @ItemID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
		[LastModifiedDate]

	FROM [dbo].[Item]
	
	WHERE
		[ItemID] = @ItemID
	
	RETURN -- update successful
END

