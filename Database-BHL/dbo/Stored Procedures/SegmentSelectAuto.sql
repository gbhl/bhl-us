
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SegmentSelectAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[SegmentSelectAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Select Procedure for dbo.Segment
-- Do not modify the contents of this procedure.
-- Generated 6/2/2016 9:32:37 AM

CREATE PROCEDURE [dbo].[SegmentSelectAuto]

@SegmentID INT

AS 

SET NOCOUNT ON

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

