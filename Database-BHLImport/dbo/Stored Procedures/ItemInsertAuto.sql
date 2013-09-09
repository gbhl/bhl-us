
-- ItemInsertAuto PROCEDURE
-- Generated 12/18/2008 2:14:22 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for Item

CREATE PROCEDURE ItemInsertAuto

@ItemID INT OUTPUT,
@ImportKey NVARCHAR(50),
@ImportStatusID INT,
@ImportSourceID INT = null,
@MARCBibID NVARCHAR(50),
@BarCode NVARCHAR(40),
@ItemSequence SMALLINT = null,
@MARCItemID NVARCHAR(50) = null,
@CallNumber NVARCHAR(100) = null,
@Volume NVARCHAR(100) = null,
@InstitutionCode NVARCHAR(10) = null,
@LanguageCode NVARCHAR(10) = null,
@Sponsor NVARCHAR(100) = null,
@ItemDescription NTEXT = null,
@ScannedBy INT = null,
@PDFSize INT = null,
@VaultID INT = null,
@NumberOfFiles SMALLINT = null,
@Note NVARCHAR(255) = null,
@ItemStatusID INT,
@ScanningUser NVARCHAR(100) = null,
@ScanningDate DATETIME = null,
@Year NVARCHAR(20) = null,
@IdentifierBib NVARCHAR(50) = null,
@ZQuery NVARCHAR(200) = null,
@LicenseUrl NVARCHAR(MAX) = null,
@Rights NVARCHAR(MAX) = null,
@DueDiligence NVARCHAR(MAX) = null,
@CopyrightStatus NVARCHAR(MAX) = null,
@CopyrightRegion NVARCHAR(50) = null,
@CopyrightComment NVARCHAR(MAX) = null,
@CopyrightEvidence NVARCHAR(MAX) = null,
@CopyrightEvidenceOperator NVARCHAR(100) = null,
@CopyrightEvidenceDate NVARCHAR(30) = null,
@PaginationCompleteUserID INT = null,
@PaginationCompleteDate DATETIME = null,
@PaginationStatusID INT = null,
@PaginationStatusUserID INT = null,
@PaginationStatusDate DATETIME = null,
@LastPageNameLookupDate DATETIME = null,
@ExternalCreationDate DATETIME = null,
@ExternalLastModifiedDate DATETIME = null,
@ExternalCreationUser INT = null,
@ExternalLastModifiedUser INT = null,
@ProductionDate DATETIME = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Item]
(
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
)
VALUES
(
	@ImportKey,
	@ImportStatusID,
	@ImportSourceID,
	@MARCBibID,
	@BarCode,
	@ItemSequence,
	@MARCItemID,
	@CallNumber,
	@Volume,
	@InstitutionCode,
	@LanguageCode,
	@Sponsor,
	@ItemDescription,
	@ScannedBy,
	@PDFSize,
	@VaultID,
	@NumberOfFiles,
	@Note,
	@ItemStatusID,
	@ScanningUser,
	@ScanningDate,
	@Year,
	@IdentifierBib,
	@ZQuery,
	@LicenseUrl,
	@Rights,
	@DueDiligence,
	@CopyrightStatus,
	@CopyrightRegion,
	@CopyrightComment,
	@CopyrightEvidence,
	@CopyrightEvidenceOperator,
	@CopyrightEvidenceDate,
	@PaginationCompleteUserID,
	@PaginationCompleteDate,
	@PaginationStatusID,
	@PaginationStatusUserID,
	@PaginationStatusDate,
	@LastPageNameLookupDate,
	@ExternalCreationDate,
	@ExternalLastModifiedDate,
	@ExternalCreationUser,
	@ExternalLastModifiedUser,
	@ProductionDate,
	getdate(),
	getdate()
)

SET @ItemID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

