
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SegmentUpdateAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[SegmentUpdateAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Update Procedure for dbo.Segment
-- Do not modify the contents of this procedure.
-- Generated 6/2/2016 9:32:37 AM

CREATE PROCEDURE dbo.SegmentUpdateAuto

@SegmentID INT,
@ItemID INT,
@SegmentStatusID INT,
@SequenceOrder SMALLINT,
@SegmentGenreID INT,
@Title NVARCHAR(2000),
@TranslatedTitle NVARCHAR(2000),
@ContainerTitle NVARCHAR(2000),
@PublicationDetails NVARCHAR(400),
@PublisherName NVARCHAR(250),
@PublisherPlace NVARCHAR(150),
@Notes NVARCHAR(MAX),
@Summary NVARCHAR(MAX),
@Volume NVARCHAR(100),
@Series NVARCHAR(100),
@Issue NVARCHAR(100),
@Edition NVARCHAR(400),
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
	[SequenceOrder] = @SequenceOrder,
	[SegmentGenreID] = @SegmentGenreID,
	[Title] = @Title,
	[TranslatedTitle] = @TranslatedTitle,
	[ContainerTitle] = @ContainerTitle,
	[PublicationDetails] = @PublicationDetails,
	[PublisherName] = @PublisherName,
	[PublisherPlace] = @PublisherPlace,
	[Notes] = @Notes,
	[Summary] = @Summary,
	[Volume] = @Volume,
	[Series] = @Series,
	[Issue] = @Issue,
	[Edition] = @Edition,
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
	RAISERROR('An error occurred in procedure dbo.SegmentUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[SegmentID],
		[ItemID],
		[SegmentStatusID],
		[SequenceOrder],
		[SegmentGenreID],
		[Title],
		[TranslatedTitle],
		[ContainerTitle],
		[PublicationDetails],
		[PublisherName],
		[PublisherPlace],
		[Notes],
		[Summary],
		[Volume],
		[Series],
		[Issue],
		[Edition],
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
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

