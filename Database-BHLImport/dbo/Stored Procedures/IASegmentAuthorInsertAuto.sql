CREATE PROCEDURE dbo.IASegmentAuthorInsertAuto

@SegmentAuthorID INT OUTPUT,
@SegmentID INT,
@Sequence INT,
@BHLAuthorID INT = null,
@FullName NVARCHAR(300),
@LastName NVARCHAR(150),
@FirstName NVARCHAR(150),
@StartDate NVARCHAR(25),
@EndDate NVARCHAR(25),
@BHLIdentifierID INT = null,
@IdentifierValue NVARCHAR(125)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[IASegmentAuthor]
( 	[SegmentID],
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
	[LastModifiedDate] )
VALUES
( 	@SegmentID,
	@Sequence,
	@BHLAuthorID,
	@FullName,
	@LastName,
	@FirstName,
	@StartDate,
	@EndDate,
	@BHLIdentifierID,
	@IdentifierValue,
	getdate(),
	getdate() )

SET @SegmentAuthorID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.IASegmentAuthorInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	FROM [dbo].[IASegmentAuthor]
	WHERE
		[SegmentAuthorID] = @SegmentAuthorID
	
	RETURN -- insert successful
END
