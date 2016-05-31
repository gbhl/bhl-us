
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[ItemUpdateAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[ItemUpdateAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Update Procedure for dbo.Item
-- Do not modify the contents of this procedure.
-- Generated 6/2/2016 9:32:20 AM

CREATE PROCEDURE dbo.ItemUpdateAuto

@ItemID INT,
@PrimaryTitleID INT,
@BarCode NVARCHAR(40),
@MARCItemID NVARCHAR(50),
@CallNumber NVARCHAR(100),
@Volume NVARCHAR(100),
@LanguageCode NVARCHAR(10),
@ItemDescription NTEXT,
@ScannedBy INT,
@PDFSize INT,
@VaultID INT,
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
@ThumbnailPageID INT,
@RedirectItemID INT,
@ExternalUrl NVARCHAR(500),
@EndYear NVARCHAR(20),
@StartVolume NVARCHAR(10),
@EndVolume NVARCHAR(10),
@StartIssue NVARCHAR(10),
@EndIssue NVARCHAR(10),
@StartNumber NVARCHAR(10),
@EndNumber NVARCHAR(10),
@StartSeries NVARCHAR(10),
@EndSeries NVARCHAR(10),
@StartPart NVARCHAR(10),
@EndPart NVARCHAR(10),
@VolumeReviewed TINYINT,
@VolumeReviewedDate DATETIME,
@VolumeReviewedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[Item]
SET
	[PrimaryTitleID] = @PrimaryTitleID,
	[BarCode] = @BarCode,
	[MARCItemID] = @MARCItemID,
	[CallNumber] = @CallNumber,
	[Volume] = @Volume,
	[LanguageCode] = @LanguageCode,
	[ItemDescription] = @ItemDescription,
	[ScannedBy] = @ScannedBy,
	[PDFSize] = @PDFSize,
	[VaultID] = @VaultID,
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
	[ThumbnailPageID] = @ThumbnailPageID,
	[RedirectItemID] = @RedirectItemID,
	[ExternalUrl] = @ExternalUrl,
	[EndYear] = @EndYear,
	[StartVolume] = @StartVolume,
	[EndVolume] = @EndVolume,
	[StartIssue] = @StartIssue,
	[EndIssue] = @EndIssue,
	[StartNumber] = @StartNumber,
	[EndNumber] = @EndNumber,
	[StartSeries] = @StartSeries,
	[EndSeries] = @EndSeries,
	[StartPart] = @StartPart,
	[EndPart] = @EndPart,
	[VolumeReviewed] = @VolumeReviewed,
	[VolumeReviewedDate] = @VolumeReviewedDate,
	[VolumeReviewedUserID] = @VolumeReviewedUserID
WHERE
	[ItemID] = @ItemID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.ItemUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[ItemID],
		[PrimaryTitleID],
		[BarCode],
		[MARCItemID],
		[CallNumber],
		[Volume],
		[LanguageCode],
		[ItemDescription],
		[ScannedBy],
		[PDFSize],
		[VaultID],
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
		[ThumbnailPageID],
		[RedirectItemID],
		[ExternalUrl],
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
		[VolumeReviewed],
		[VolumeReviewedDate],
		[VolumeReviewedUserID]
	FROM [dbo].[Item]
	WHERE
		[ItemID] = @ItemID
	
	RETURN -- update successful
END
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

