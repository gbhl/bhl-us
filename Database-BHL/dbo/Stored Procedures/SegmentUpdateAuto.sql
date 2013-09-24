
-- SegmentUpdateAuto PROCEDURE
-- Generated 9/24/2013 2:29:04 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for Segment

CREATE PROCEDURE SegmentUpdateAuto

@SegmentID INT,
@ItemID INT,
@SegmentStatusID INT,
@ContributorCode NVARCHAR(10),
@ContributorSegmentID NVARCHAR(100),
@SequenceOrder SMALLINT,
@SegmentGenreID INT,
@Title NVARCHAR(2000),
@TranslatedTitle NVARCHAR(2000),
@ContainerTitle NVARCHAR(2000),
@PublicationDetails NVARCHAR(400),
@PublisherName NVARCHAR(250),
@PublisherPlace NVARCHAR(150),
@Notes NVARCHAR(MAX),
@Volume NVARCHAR(100),
@Series NVARCHAR(100),
@Issue NVARCHAR(100),
@Date NVARCHAR(20),
@PageRange NVARCHAR(50),
@StartPageNumber NVARCHAR(20),
@EndPageNumber NVARCHAR(20),
@StartPageID INT,
@LanguageCode NVARCHAR(10),
@Url NVARCHAR(200),
@DownloadUrl NVARCHAR(200),
@RightsStatus NVARCHAR(500),
@RightsStatement NVARCHAR(500),
@LicenseName NVARCHAR(200),
@LicenseUrl NVARCHAR(200),
@ContributorCreationDate DATETIME,
@ContributorLastModifiedDate DATETIME,
@LastModifiedUserID INT,
@SortTitle NVARCHAR(2000),
@RedirectSegmentID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[Segment]

SET

	[ItemID] = @ItemID,
	[SegmentStatusID] = @SegmentStatusID,
	[ContributorCode] = @ContributorCode,
	[ContributorSegmentID] = @ContributorSegmentID,
	[SequenceOrder] = @SequenceOrder,
	[SegmentGenreID] = @SegmentGenreID,
	[Title] = @Title,
	[TranslatedTitle] = @TranslatedTitle,
	[ContainerTitle] = @ContainerTitle,
	[PublicationDetails] = @PublicationDetails,
	[PublisherName] = @PublisherName,
	[PublisherPlace] = @PublisherPlace,
	[Notes] = @Notes,
	[Volume] = @Volume,
	[Series] = @Series,
	[Issue] = @Issue,
	[Date] = @Date,
	[PageRange] = @PageRange,
	[StartPageNumber] = @StartPageNumber,
	[EndPageNumber] = @EndPageNumber,
	[StartPageID] = @StartPageID,
	[LanguageCode] = @LanguageCode,
	[Url] = @Url,
	[DownloadUrl] = @DownloadUrl,
	[RightsStatus] = @RightsStatus,
	[RightsStatement] = @RightsStatement,
	[LicenseName] = @LicenseName,
	[LicenseUrl] = @LicenseUrl,
	[ContributorCreationDate] = @ContributorCreationDate,
	[ContributorLastModifiedDate] = @ContributorLastModifiedDate,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID,
	[SortTitle] = @SortTitle,
	[RedirectSegmentID] = @RedirectSegmentID

WHERE
	[SegmentID] = @SegmentID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[SegmentID],
		[ItemID],
		[SegmentStatusID],
		[ContributorCode],
		[ContributorSegmentID],
		[SequenceOrder],
		[SegmentGenreID],
		[Title],
		[TranslatedTitle],
		[ContainerTitle],
		[PublicationDetails],
		[PublisherName],
		[PublisherPlace],
		[Notes],
		[Volume],
		[Series],
		[Issue],
		[Date],
		[PageRange],
		[StartPageNumber],
		[EndPageNumber],
		[StartPageID],
		[LanguageCode],
		[Url],
		[DownloadUrl],
		[RightsStatus],
		[RightsStatement],
		[LicenseName],
		[LicenseUrl],
		[ContributorCreationDate],
		[ContributorLastModifiedDate],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID],
		[SortTitle],
		[RedirectSegmentID]

	FROM [dbo].[Segment]
	
	WHERE
		[SegmentID] = @SegmentID
	
	RETURN -- update successful
END

