
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SegmentSelectAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[SegmentSelectAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE PROCEDURE [dbo].[SegmentSelectAuto]

@SegmentID INT

AS 

SET NOCOUNT ON

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
FROM	
	[dbo].[Segment]
WHERE	
	[SegmentID] = @SegmentID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.SegmentSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

