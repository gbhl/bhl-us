-- Select Procedure for dbo.BSSegment
-- Do not modify the contents of this procedure.
-- Generated 11/21/2016 1:39:33 PM

CREATE PROCEDURE [dbo].[BSSegmentSelectAuto]

@SegmentID INT

AS 

SET NOCOUNT ON

SELECT	
	[SegmentID],
	[ItemID],
	[BioStorReferenceID],
	[SequenceOrder],
	[Genre],
	[Title],
	[ContainerTitle],
	[PublisherName],
	[PublisherPlace],
	[Volume],
	[Series],
	[Issue],
	[Year],
	[Date],
	[ISSN],
	[DOI],
	[OCLC],
	[JSTOR],
	[StartPageNumber],
	[EndPageNumber],
	[StartPageID],
	[ContributorCreationDate],
	[ContributorLastModifiedDate],
	[BHLSegmentID],
	[CreationDate],
	[LastModifiedDate]
FROM	
	[dbo].[BSSegment]
WHERE	
	[SegmentID] = @SegmentID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.BSSegmentSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
