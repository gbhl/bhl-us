CREATE PROCEDURE dbo.IASegmentInsertAuto

@SegmentID INT OUTPUT,
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

INSERT INTO [dbo].[IASegment]
( 	[ItemID],
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
	[LastModifiedDate] )
VALUES
( 	@ItemID,
	@Sequence,
	@Title,
	@Volume,
	@Issue,
	@Series,
	@Date,
	@LanguageCode,
	@BHLSegmentGenreID,
	@BHLSegmentGenreName,
	@DOI,
	getdate(),
	getdate() )

SET @SegmentID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.IASegmentInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END
