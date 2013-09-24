
-- SegmentInsertAuto PROCEDURE
-- Generated 9/24/2013 2:29:04 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for Segment

CREATE PROCEDURE SegmentInsertAuto

@SegmentID INT OUTPUT,
@ItemID INT = null,
@SegmentStatusID INT,
@ContributorCode NVARCHAR(10) = null,
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
@StartPageID INT = null,
@LanguageCode NVARCHAR(10) = null,
@Url NVARCHAR(200),
@DownloadUrl NVARCHAR(200),
@RightsStatus NVARCHAR(500),
@RightsStatement NVARCHAR(500),
@LicenseName NVARCHAR(200),
@LicenseUrl NVARCHAR(200),
@ContributorCreationDate DATETIME = null,
@ContributorLastModifiedDate DATETIME = null,
@CreationUserID INT = null,
@LastModifiedUserID INT = null,
@SortTitle NVARCHAR(2000),
@RedirectSegmentID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Segment]
(
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
)
VALUES
(
	@ItemID,
	@SegmentStatusID,
	@ContributorCode,
	@ContributorSegmentID,
	@SequenceOrder,
	@SegmentGenreID,
	@Title,
	@TranslatedTitle,
	@ContainerTitle,
	@PublicationDetails,
	@PublisherName,
	@PublisherPlace,
	@Notes,
	@Volume,
	@Series,
	@Issue,
	@Date,
	@PageRange,
	@StartPageNumber,
	@EndPageNumber,
	@StartPageID,
	@LanguageCode,
	@Url,
	@DownloadUrl,
	@RightsStatus,
	@RightsStatement,
	@LicenseName,
	@LicenseUrl,
	@ContributorCreationDate,
	@ContributorLastModifiedDate,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID,
	@SortTitle,
	@RedirectSegmentID
)

SET @SegmentID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

