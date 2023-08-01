
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SegmentInsertAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[SegmentInsertAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE PROCEDURE dbo.SegmentInsertAuto

@SegmentID INT OUTPUT,
@ItemID INT,
@RedirectSegmentID INT = null,
@SegmentGenreID INT,
@StartPageID INT = null,
@ThumbnailPageID INT = null,
@LanguageCode NVARCHAR(10) = null,
@BarCode NVARCHAR(200) = null,
@MARCItemID NVARCHAR(200) = null,
@Title NVARCHAR(2000),
@SortTitle NVARCHAR(2000),
@TranslatedTitle NVARCHAR(2000),
@ContainerTitle NVARCHAR(2000),
@PublicationDetails NVARCHAR(400),
@PublisherName NVARCHAR(250),
@PublisherPlace NVARCHAR(150),
@Summary NVARCHAR(MAX),
@Volume NVARCHAR(100),
@Series NVARCHAR(100),
@Issue NVARCHAR(100),
@Edition NVARCHAR(400),
@Date NVARCHAR(20),
@PageRange NVARCHAR(50),
@StartPageNumber NVARCHAR(20),
@EndPageNumber NVARCHAR(20),
@Url NVARCHAR(200),
@DownloadUrl NVARCHAR(200),
@LicenseName NVARCHAR(200),
@LicenseUrl NVARCHAR(200),
@RightsStatus NVARCHAR(500),
@RightsStatement NVARCHAR(MAX),
@CopyrightStatus NVARCHAR(MAX),
@CopyrightRegion NVARCHAR(50),
@CopyrightComment NVARCHAR(MAX),
@CopyrightEvidence NVARCHAR(MAX),
@ScanningUser NVARCHAR(100) = null,
@ScanningDate DATETIME = null,
@PaginationStatusID INT = null,
@PaginationStatusDate DATETIME = null,
@PaginationStatusUserID INT = null,
@PaginationCompleteDate DATETIME = null,
@PaginationCompleteUserID INT = null,
@LastPageNameLookupDate DATETIME = null,
@CreationUserID INT = null,
@LastModifiedUserID INT = null,
@PageProgression NVARCHAR(10),
@PreferredContainerTitleID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Segment]
( 	[ItemID],
	[RedirectSegmentID],
	[SegmentGenreID],
	[StartPageID],
	[ThumbnailPageID],
	[LanguageCode],
	[BarCode],
	[MARCItemID],
	[Title],
	[SortTitle],
	[TranslatedTitle],
	[ContainerTitle],
	[PublicationDetails],
	[PublisherName],
	[PublisherPlace],
	[Summary],
	[Volume],
	[Series],
	[Issue],
	[Edition],
	[Date],
	[PageRange],
	[StartPageNumber],
	[EndPageNumber],
	[Url],
	[DownloadUrl],
	[LicenseName],
	[LicenseUrl],
	[RightsStatus],
	[RightsStatement],
	[CopyrightStatus],
	[CopyrightRegion],
	[CopyrightComment],
	[CopyrightEvidence],
	[ScanningUser],
	[ScanningDate],
	[PaginationStatusID],
	[PaginationStatusDate],
	[PaginationStatusUserID],
	[PaginationCompleteDate],
	[PaginationCompleteUserID],
	[LastPageNameLookupDate],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID],
	[PageProgression],
	[PreferredContainerTitleID] )
VALUES
( 	@ItemID,
	@RedirectSegmentID,
	@SegmentGenreID,
	@StartPageID,
	@ThumbnailPageID,
	@LanguageCode,
	@BarCode,
	@MARCItemID,
	@Title,
	@SortTitle,
	@TranslatedTitle,
	@ContainerTitle,
	@PublicationDetails,
	@PublisherName,
	@PublisherPlace,
	@Summary,
	@Volume,
	@Series,
	@Issue,
	@Edition,
	@Date,
	@PageRange,
	@StartPageNumber,
	@EndPageNumber,
	@Url,
	@DownloadUrl,
	@LicenseName,
	@LicenseUrl,
	@RightsStatus,
	@RightsStatement,
	@CopyrightStatus,
	@CopyrightRegion,
	@CopyrightComment,
	@CopyrightEvidence,
	@ScanningUser,
	@ScanningDate,
	@PaginationStatusID,
	@PaginationStatusDate,
	@PaginationStatusUserID,
	@PaginationCompleteDate,
	@PaginationCompleteUserID,
	@LastPageNameLookupDate,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID,
	@PageProgression,
	@PreferredContainerTitleID )

SET @SegmentID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.SegmentInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[SegmentID],
		[ItemID],
		[RedirectSegmentID],
		[SegmentGenreID],
		[StartPageID],
		[ThumbnailPageID],
		[LanguageCode],
		[BarCode],
		[MARCItemID],
		[Title],
		[SortTitle],
		[TranslatedTitle],
		[ContainerTitle],
		[PublicationDetails],
		[PublisherName],
		[PublisherPlace],
		[Summary],
		[Volume],
		[Series],
		[Issue],
		[Edition],
		[Date],
		[PageRange],
		[StartPageNumber],
		[EndPageNumber],
		[Url],
		[DownloadUrl],
		[LicenseName],
		[LicenseUrl],
		[RightsStatus],
		[RightsStatement],
		[CopyrightStatus],
		[CopyrightRegion],
		[CopyrightComment],
		[CopyrightEvidence],
		[ScanningUser],
		[ScanningDate],
		[PaginationStatusID],
		[PaginationStatusDate],
		[PaginationStatusUserID],
		[PaginationCompleteDate],
		[PaginationCompleteUserID],
		[LastPageNameLookupDate],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID],
		[PageProgression],
		[PreferredContainerTitleID]	
	FROM [dbo].[Segment]
	WHERE
		[SegmentID] = @SegmentID
	
	RETURN -- insert successful
END
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

