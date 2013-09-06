
-- ItemInsertAuto PROCEDURE
-- Generated 6/18/2013 3:43:19 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for Item

CREATE PROCEDURE ItemInsertAuto

@ItemID INT OUTPUT,
@PrimaryTitleID INT,
@RedirectItemID INT = null,
@ThumbnailPageID INT = null,
@BarCode NVARCHAR(40),
@MARCItemID NVARCHAR(50) = null,
@CallNumber NVARCHAR(100) = null,
@Volume NVARCHAR(100) = null,
@InstitutionCode NVARCHAR(10) = null,
@LanguageCode NVARCHAR(10) = null,
@ItemDescription NTEXT = null,
@ScannedBy INT = null,
@PDFSize INT = null,
@VaultID INT = null,
@NumberOfFiles SMALLINT = null,
@Note NVARCHAR(255) = null,
@CreationUserID INT = null,
@LastModifiedUserID INT = null,
@ItemStatusID INT,
@ScanningUser NVARCHAR(100) = null,
@ScanningDate DATETIME = null,
@PaginationCompleteUserID INT = null,
@PaginationCompleteDate DATETIME = null,
@PaginationStatusID INT = null,
@PaginationStatusUserID INT = null,
@PaginationStatusDate DATETIME = null,
@LastPageNameLookupDate DATETIME = null,
@ItemSourceID INT = null,
@Year NVARCHAR(20) = null,
@IdentifierBib NVARCHAR(50) = null,
@FileRootFolder NVARCHAR(250) = null,
@ZQuery NVARCHAR(200) = null,
@Sponsor NVARCHAR(100) = null,
@LicenseUrl NVARCHAR(MAX) = null,
@Rights NVARCHAR(MAX) = null,
@DueDiligence NVARCHAR(MAX) = null,
@CopyrightStatus NVARCHAR(MAX) = null,
@CopyrightRegion NVARCHAR(50) = null,
@CopyrightComment NVARCHAR(MAX) = null,
@CopyrightEvidence NVARCHAR(MAX) = null,
@CopyrightEvidenceOperator NVARCHAR(100) = null,
@CopyrightEvidenceDate NVARCHAR(30) = null,
@ExternalUrl NVARCHAR(500) = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Item]
(
	[PrimaryTitleID],
	[RedirectItemID],
	[ThumbnailPageID],
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
	[ExternalUrl]
)
VALUES
(
	@PrimaryTitleID,
	@RedirectItemID,
	@ThumbnailPageID,
	@BarCode,
	@MARCItemID,
	@CallNumber,
	@Volume,
	@InstitutionCode,
	@LanguageCode,
	@ItemDescription,
	@ScannedBy,
	@PDFSize,
	@VaultID,
	@NumberOfFiles,
	@Note,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID,
	@ItemStatusID,
	@ScanningUser,
	@ScanningDate,
	@PaginationCompleteUserID,
	@PaginationCompleteDate,
	@PaginationStatusID,
	@PaginationStatusUserID,
	@PaginationStatusDate,
	@LastPageNameLookupDate,
	@ItemSourceID,
	@Year,
	@IdentifierBib,
	@FileRootFolder,
	@ZQuery,
	@Sponsor,
	@LicenseUrl,
	@Rights,
	@DueDiligence,
	@CopyrightStatus,
	@CopyrightRegion,
	@CopyrightComment,
	@CopyrightEvidence,
	@CopyrightEvidenceOperator,
	@CopyrightEvidenceDate,
	@ExternalUrl
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
		[PrimaryTitleID],
		[RedirectItemID],
		[ThumbnailPageID],
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
		[ExternalUrl]	

	FROM [dbo].[Item]
	
	WHERE
		[ItemID] = @ItemID
	
	RETURN -- insert successful
END

