
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SegmentUpdateAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[SegmentUpdateAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE PROCEDURE dbo.SegmentUpdateAuto

@SegmentID INT,
@ItemID INT,
@RedirectSegmentID INT,
@SegmentGenreID INT,
@StartPageID INT,
@ThumbnailPageID INT,
@LanguageCode NVARCHAR(10),
@BarCode NVARCHAR(200),
@MARCItemID NVARCHAR(200),
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
@ScanningUser NVARCHAR(100),
@ScanningDate DATETIME,
@PaginationStatusID INT,
@PaginationStatusDate DATETIME,
@PaginationStatusUserID INT,
@PaginationCompleteDate DATETIME,
@PaginationCompleteUserID INT,
@LastPageNameLookupDate DATETIME,
@LastModifiedUserID INT,
@PageProgression NVARCHAR(10),
@PreferredContainerTitleID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[Segment]
SET
	[ItemID] = @ItemID,
	[RedirectSegmentID] = @RedirectSegmentID,
	[SegmentGenreID] = @SegmentGenreID,
	[StartPageID] = @StartPageID,
	[ThumbnailPageID] = @ThumbnailPageID,
	[LanguageCode] = @LanguageCode,
	[BarCode] = @BarCode,
	[MARCItemID] = @MARCItemID,
	[Title] = @Title,
	[SortTitle] = @SortTitle,
	[TranslatedTitle] = @TranslatedTitle,
	[ContainerTitle] = @ContainerTitle,
	[PublicationDetails] = @PublicationDetails,
	[PublisherName] = @PublisherName,
	[PublisherPlace] = @PublisherPlace,
	[Summary] = @Summary,
	[Volume] = @Volume,
	[Series] = @Series,
	[Issue] = @Issue,
	[Edition] = @Edition,
	[Date] = @Date,
	[PageRange] = @PageRange,
	[StartPageNumber] = @StartPageNumber,
	[EndPageNumber] = @EndPageNumber,
	[Url] = @Url,
	[DownloadUrl] = @DownloadUrl,
	[LicenseName] = @LicenseName,
	[LicenseUrl] = @LicenseUrl,
	[RightsStatus] = @RightsStatus,
	[RightsStatement] = @RightsStatement,
	[CopyrightStatus] = @CopyrightStatus,
	[CopyrightRegion] = @CopyrightRegion,
	[CopyrightComment] = @CopyrightComment,
	[CopyrightEvidence] = @CopyrightEvidence,
	[ScanningUser] = @ScanningUser,
	[ScanningDate] = @ScanningDate,
	[PaginationStatusID] = @PaginationStatusID,
	[PaginationStatusDate] = @PaginationStatusDate,
	[PaginationStatusUserID] = @PaginationStatusUserID,
	[PaginationCompleteDate] = @PaginationCompleteDate,
	[PaginationCompleteUserID] = @PaginationCompleteUserID,
	[LastPageNameLookupDate] = @LastPageNameLookupDate,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID,
	[PageProgression] = @PageProgression,
	[PreferredContainerTitleID] = @PreferredContainerTitleID
WHERE
	[SegmentID] = @SegmentID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.SegmentUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

