CREATE PROCEDURE dbo.BSSegmentInsertAuto

@SegmentID INT OUTPUT,
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
@StartPageID INT = null,
@ContributorCreationDate DATETIME = null,
@ContributorLastModifiedDate DATETIME = null,
@BHLSegmentID INT = null,
@ContributorName NVARCHAR(255),
@SegmentStatusID INT

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[BSSegment]
( 	[ItemID],
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
	[SegmentStatusID] )
VALUES
( 	@ItemID,
	@BioStorReferenceID,
	@SequenceOrder,
	@Genre,
	@Title,
	@ContainerTitle,
	@PublisherName,
	@PublisherPlace,
	@Volume,
	@Series,
	@Issue,
	@Year,
	@Date,
	@ISSN,
	@DOI,
	@OCLC,
	@JSTOR,
	@StartPageNumber,
	@EndPageNumber,
	@StartPageID,
	@ContributorCreationDate,
	@ContributorLastModifiedDate,
	@BHLSegmentID,
	getdate(),
	getdate(),
	@ContributorName,
	@SegmentStatusID )

SET @SegmentID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.BSSegmentInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END
