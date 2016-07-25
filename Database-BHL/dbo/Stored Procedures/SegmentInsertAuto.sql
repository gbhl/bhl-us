
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SegmentInsertAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[SegmentInsertAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Insert Procedure for dbo.Segment
-- Do not modify the contents of this procedure.
-- Generated 6/2/2016 9:32:37 AM

CREATE PROCEDURE dbo.SegmentInsertAuto

@SegmentID INT OUTPUT,
@ItemID INT = null,
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
( 	[ItemID],
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
	[RedirectSegmentID] )
VALUES
( 	@ItemID,
	@SegmentStatusID,
	@SequenceOrder,
	@SegmentGenreID,
	@Title,
	@TranslatedTitle,
	@ContainerTitle,
	@PublicationDetails,
	@PublisherName,
	@PublisherPlace,
	@Notes,
	@Summary,
	@Volume,
	@Series,
	@Issue,
	@Edition,
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
	@RedirectSegmentID )

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
	
	RETURN -- insert successful
END
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

