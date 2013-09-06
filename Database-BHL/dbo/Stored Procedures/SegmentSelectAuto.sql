
-- SegmentSelectAuto PROCEDURE
-- Generated 4/12/2013 11:25:53 AM
-- Do not modify the contents of this procedure.
-- Select Procedure for Segment

CREATE PROCEDURE SegmentSelectAuto

@SegmentID INT

AS 

SET NOCOUNT ON

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
	[SortTitle]

FROM [dbo].[Segment]

WHERE
	[SegmentID] = @SegmentID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

