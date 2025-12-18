CREATE PROCEDURE dbo.BSSegmentAuthorInsertAuto

@SegmentAuthorID INT OUTPUT,
@ImportSourceID INT,
@SegmentID INT,
@BioStorID NVARCHAR(100),
@LastName NVARCHAR(150),
@FirstName NVARCHAR(150),
@SequenceOrder INT,
@VIAFIdentifier NVARCHAR(20),
@BHLAuthorID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[BSSegmentAuthor]
( 	[ImportSourceID],
	[SegmentID],
	[BioStorID],
	[LastName],
	[FirstName],
	[SequenceOrder],
	[VIAFIdentifier],
	[BHLAuthorID],
	[CreationDate],
	[LastModifiedDate] )
VALUES
( 	@ImportSourceID,
	@SegmentID,
	@BioStorID,
	@LastName,
	@FirstName,
	@SequenceOrder,
	@VIAFIdentifier,
	@BHLAuthorID,
	getdate(),
	getdate() )

SET @SegmentAuthorID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.BSSegmentAuthorInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[SegmentAuthorID],
		[ImportSourceID],
		[SegmentID],
		[BioStorID],
		[LastName],
		[FirstName],
		[SequenceOrder],
		[VIAFIdentifier],
		[BHLAuthorID],
		[CreationDate],
		[LastModifiedDate]	
	FROM [dbo].[BSSegmentAuthor]
	WHERE
		[SegmentAuthorID] = @SegmentAuthorID
	
	RETURN -- insert successful
END
GO
