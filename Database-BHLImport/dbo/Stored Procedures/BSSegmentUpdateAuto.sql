
-- BSSegmentUpdateAuto PROCEDURE
-- Generated 10/24/2012 4:21:54 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for BSSegment

CREATE PROCEDURE BSSegmentUpdateAuto

@SegmentID INT,
@ItemID INT,
@BioStorReferenceID NVARCHAR(100),
@SequenceOrder SMALLINT,
@Genre NVARCHAR(50),
@Title NVARCHAR(2000),
@ContainerTitle NVARCHAR(2000),
@Volume NVARCHAR(100),
@Series NVARCHAR(100),
@Issue NVARCHAR(100),
@Year NVARCHAR(20),
@Date NVARCHAR(20),
@ISSN NVARCHAR(125),
@DOI NVARCHAR(50),
@StartPageNumber NVARCHAR(20),
@EndPageNumber NVARCHAR(20),
@StartPageID INT,
@ContributorCreationDate DATETIME,
@ContributorLastModifiedDate DATETIME,
@BHLSegmentID INT

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
	[Volume] = @Volume,
	[Series] = @Series,
	[Issue] = @Issue,
	[Year] = @Year,
	[Date] = @Date,
	[ISSN] = @ISSN,
	[DOI] = @DOI,
	[StartPageNumber] = @StartPageNumber,
	[EndPageNumber] = @EndPageNumber,
	[StartPageID] = @StartPageID,
	[ContributorCreationDate] = @ContributorCreationDate,
	[ContributorLastModifiedDate] = @ContributorLastModifiedDate,
	[BHLSegmentID] = @BHLSegmentID,
	[LastModifiedDate] = getdate()

WHERE
	[SegmentID] = @SegmentID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure BSSegmentUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

