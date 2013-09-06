
-- ItemUpdateAuto PROCEDURE
-- Generated 6/18/2013 3:43:19 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for Item

CREATE PROCEDURE ItemUpdateAuto

@ItemID INT,
@PrimaryTitleID INT,
@RedirectItemID INT,
@ThumbnailPageID INT,
@BarCode NVARCHAR(40),
@MARCItemID NVARCHAR(50),
@CallNumber NVARCHAR(100),
@Volume NVARCHAR(100),
@InstitutionCode NVARCHAR(10),
@LanguageCode NVARCHAR(10),
@ItemDescription NTEXT,
@ScannedBy INT,
@PDFSize INT,
@VaultID INT,
@NumberOfFiles SMALLINT,
@Note NVARCHAR(255),
@LastModifiedUserID INT,
@ItemStatusID INT,
@ScanningUser NVARCHAR(100),
@ScanningDate DATETIME,
@PaginationCompleteUserID INT,
@PaginationCompleteDate DATETIME,
@PaginationStatusID INT,
@PaginationStatusUserID INT,
@PaginationStatusDate DATETIME,
@LastPageNameLookupDate DATETIME,
@ItemSourceID INT,
@Year NVARCHAR(20),
@IdentifierBib NVARCHAR(50),
@FileRootFolder NVARCHAR(250),
@ZQuery NVARCHAR(200),
@Sponsor NVARCHAR(100),
@LicenseUrl NVARCHAR(MAX),
@Rights NVARCHAR(MAX),
@DueDiligence NVARCHAR(MAX),
@CopyrightStatus NVARCHAR(MAX),
@CopyrightRegion NVARCHAR(50),
@CopyrightComment NVARCHAR(MAX),
@CopyrightEvidence NVARCHAR(MAX),
@CopyrightEvidenceOperator NVARCHAR(100),
@CopyrightEvidenceDate NVARCHAR(30),
@ExternalUrl NVARCHAR(500)

AS 

SET NOCOUNT ON

UPDATE [dbo].[Item]

SET

	[PrimaryTitleID] = @PrimaryTitleID,
	[RedirectItemID] = @RedirectItemID,
	[ThumbnailPageID] = @ThumbnailPageID,
	[BarCode] = @BarCode,
	[MARCItemID] = @MARCItemID,
	[CallNumber] = @CallNumber,
	[Volume] = @Volume,
	[InstitutionCode] = @InstitutionCode,
	[LanguageCode] = @LanguageCode,
	[ItemDescription] = @ItemDescription,
	[ScannedBy] = @ScannedBy,
	[PDFSize] = @PDFSize,
	[VaultID] = @VaultID,
	[NumberOfFiles] = @NumberOfFiles,
	[Note] = @Note,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID,
	[ItemStatusID] = @ItemStatusID,
	[ScanningUser] = @ScanningUser,
	[ScanningDate] = @ScanningDate,
	[PaginationCompleteUserID] = @PaginationCompleteUserID,
	[PaginationCompleteDate] = @PaginationCompleteDate,
	[PaginationStatusID] = @PaginationStatusID,
	[PaginationStatusUserID] = @PaginationStatusUserID,
	[PaginationStatusDate] = @PaginationStatusDate,
	[LastPageNameLookupDate] = @LastPageNameLookupDate,
	[ItemSourceID] = @ItemSourceID,
	[Year] = @Year,
	[IdentifierBib] = @IdentifierBib,
	[FileRootFolder] = @FileRootFolder,
	[ZQuery] = @ZQuery,
	[Sponsor] = @Sponsor,
	[LicenseUrl] = @LicenseUrl,
	[Rights] = @Rights,
	[DueDiligence] = @DueDiligence,
	[CopyrightStatus] = @CopyrightStatus,
	[CopyrightRegion] = @CopyrightRegion,
	[CopyrightComment] = @CopyrightComment,
	[CopyrightEvidence] = @CopyrightEvidence,
	[CopyrightEvidenceOperator] = @CopyrightEvidenceOperator,
	[CopyrightEvidenceDate] = @CopyrightEvidenceDate,
	[ExternalUrl] = @ExternalUrl

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
	
	RETURN -- update successful
END

