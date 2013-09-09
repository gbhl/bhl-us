
-- BSSegmentInsertAuto PROCEDURE
-- Generated 10/24/2012 4:21:54 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for BSSegment

CREATE PROCEDURE BSSegmentInsertAuto

@SegmentID INT OUTPUT,
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
@StartPageID INT = null,
@ContributorCreationDate DATETIME = null,
@ContributorLastModifiedDate DATETIME = null,
@BHLSegmentID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[BSSegment]
(
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
)
VALUES
(
	@ItemID,
	@BioStorReferenceID,
	@SequenceOrder,
	@Genre,
	@Title,
	@ContainerTitle,
	@Volume,
	@Series,
	@Issue,
	@Year,
	@Date,
	@ISSN,
	@DOI,
	@StartPageNumber,
	@EndPageNumber,
	@StartPageID,
	@ContributorCreationDate,
	@ContributorLastModifiedDate,
	@BHLSegmentID,
	getdate(),
	getdate()
)

SET @SegmentID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure BSSegmentInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

