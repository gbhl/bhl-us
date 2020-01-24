CREATE PROCEDURE dbo.IASegmentAuthorUpdateAuto

@SegmentAuthorID INT,
@SegmentID INT,
@Sequence INT,
@BHLAuthorID INT,
@FullName NVARCHAR(300),
@LastName NVARCHAR(150),
@FirstName NVARCHAR(150),
@StartDate NVARCHAR(25),
@EndDate NVARCHAR(25),
@BHLIdentifierID INT,
@IdentifierValue NVARCHAR(125)

AS 

SET NOCOUNT ON

UPDATE [dbo].[IASegmentAuthor]
SET
	[SegmentID] = @SegmentID,
	[Sequence] = @Sequence,
	[BHLAuthorID] = @BHLAuthorID,
	[FullName] = @FullName,
	[LastName] = @LastName,
	[FirstName] = @FirstName,
	[StartDate] = @StartDate,
	[EndDate] = @EndDate,
	[BHLIdentifierID] = @BHLIdentifierID,
	[IdentifierValue] = @IdentifierValue,
	[LastModifiedDate] = getdate()
WHERE
	[SegmentAuthorID] = @SegmentAuthorID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.IASegmentAuthorUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END
