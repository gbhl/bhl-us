CREATE PROCEDURE dbo.BSSegmentUpdateAuto

@SegmentID INT,
@ItemID INT,
@BioStorReferenceID NVARCHAR(100),
@SequenceOrder SMALLINT,
@Genre NVARCHAR(50),
@Title NVARCHAR(2000),
@ContainerTitle NVARCHAR(2000),
@PublisherName NVARCHAR(250),
@PublisherPlace NVARCHAR(150),
@Volume NVARCHAR(100),
@Series NVARCHAR(100),
@Issue NVARCHAR(100),
@Year NVARCHAR(20),
@Date NVARCHAR(20),
@ISSN NVARCHAR(125),
@DOI NVARCHAR(50),
@OCLC NVARCHAR(125),
@JSTOR NVARCHAR(125),
@StartPageNumber NVARCHAR(20),
@EndPageNumber NVARCHAR(20),
@StartPageID INT,
@ContributorCreationDate DATETIME,
@ContributorLastModifiedDate DATETIME,
@BHLSegmentID INT,
@ContributorName NVARCHAR(255),
@SegmentStatusID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[BSSegment]
SET
	[ItemID] = @ItemID,
	[BioStorReferenceID] = @BioStorReferenceID,
	[SequenceOrder] = @SequenceOrder,
	[Genre] = @Genre,
	[Title] = @Title,
	[ContainerTitle] = @ContainerTitle,
	[PublisherName] = @PublisherName,
	[PublisherPlace] = @PublisherPlace,
	[Volume] = @Volume,
	[Series] = @Series,
	[Issue] = @Issue,
	[Year] = @Year,
	[Date] = @Date,
	[ISSN] = @ISSN,
	[DOI] = @DOI,
	[OCLC] = @OCLC,
	[JSTOR] = @JSTOR,
	[StartPageNumber] = @StartPageNumber,
	[EndPageNumber] = @EndPageNumber,
	[StartPageID] = @StartPageID,
	[ContributorCreationDate] = @ContributorCreationDate,
	[ContributorLastModifiedDate] = @ContributorLastModifiedDate,
	[BHLSegmentID] = @BHLSegmentID,
	[LastModifiedDate] = getdate(),
	[ContributorName] = @ContributorName,
	[SegmentStatusID] = @SegmentStatusID
WHERE
	[SegmentID] = @SegmentID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.BSSegmentUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
		[LastModifiedDate],
		[ContributorName],
		[SegmentStatusID]
	FROM [dbo].[BSSegment]
	WHERE
		[SegmentID] = @SegmentID
	
	RETURN -- update successful
END
