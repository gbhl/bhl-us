CREATE PROCEDURE [dbo].[IASegmentSelectAuto]

@SegmentID INT

AS 

SET NOCOUNT ON

SELECT	
	[SegmentID],
	[ItemID],
	[Sequence],
	[Title],
	[Volume],
	[Issue],
	[Series],
	[Date],
	[LanguageCode],
	[BHLSegmentGenreID],
	[BHLSegmentGenreName],
	[DOI],
	[CreatedDate],
	[LastModifiedDate]
FROM	
	[dbo].[IASegment]
WHERE	
	[SegmentID] = @SegmentID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.IASegmentSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
