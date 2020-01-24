CREATE PROCEDURE dbo.IASegmentUpdateAuto

@SegmentID INT,
@ItemID INT,
@Sequence INT,
@Title NVARCHAR(2000),
@Volume NVARCHAR(100),
@Issue NVARCHAR(100),
@Series NVARCHAR(100),
@Date NVARCHAR(20),
@LanguageCode NVARCHAR(10),
@BHLSegmentGenreID INT,
@BHLSegmentGenreName NVARCHAR(50),
@DOI NVARCHAR(50)

AS 

SET NOCOUNT ON

UPDATE [dbo].[IASegment]
SET
	[ItemID] = @ItemID,
	[Sequence] = @Sequence,
	[Title] = @Title,
	[Volume] = @Volume,
	[Issue] = @Issue,
	[Series] = @Series,
	[Date] = @Date,
	[LanguageCode] = @LanguageCode,
	[BHLSegmentGenreID] = @BHLSegmentGenreID,
	[BHLSegmentGenreName] = @BHLSegmentGenreName,
	[DOI] = @DOI,
	[LastModifiedDate] = getdate()
WHERE
	[SegmentID] = @SegmentID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.IASegmentUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	FROM [dbo].[IASegment]
	WHERE
		[SegmentID] = @SegmentID
	
	RETURN -- update successful
END
