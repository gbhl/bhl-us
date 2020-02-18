CREATE PROCEDURE [dbo].[IASegmentAuthorSelectAuto]

@SegmentAuthorID INT

AS 

SET NOCOUNT ON

SELECT	
	[SegmentAuthorID],
	[SegmentID],
	[Sequence],
	[BHLAuthorID],
	[FullName],
	[LastName],
	[FirstName],
	[StartDate],
	[EndDate],
	[BHLIdentifierID],
	[IdentifierValue],
	[CreatedDate],
	[LastModifiedDate]
FROM	
	[dbo].[IASegmentAuthor]
WHERE	
	[SegmentAuthorID] = @SegmentAuthorID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.IASegmentAuthorSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
