
-- BSSegmentSelectAuto PROCEDURE
-- Generated 10/24/2012 4:21:54 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for BSSegment

CREATE PROCEDURE BSSegmentSelectAuto

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
	[Volume],
	[Series],
	[Issue],
	[Year],
	[Date],
	[ISSN],
	[DOI],
	[StartPageNumber],
	[EndPageNumber],
	[StartPageID],
	[ContributorCreationDate],
	[ContributorLastModifiedDate],
	[BHLSegmentID],
	[CreationDate],
	[LastModifiedDate]

FROM [dbo].[BSSegment]

WHERE
	[SegmentID] = @SegmentID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure BSSegmentSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

