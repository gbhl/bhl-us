CREATE PROCEDURE dbo.SegmentExternalResourceInsertAuto

@SegmentExternalResourceID INT OUTPUT,
@SegmentID INT,
@ExternalResourceTypeID INT,
@UrlText NVARCHAR(100),
@Url NVARCHAR(200),
@SequenceOrder SMALLINT,
@CreationUserID INT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[SegmentExternalResource]
( 	[SegmentID],
	[ExternalResourceTypeID],
	[UrlText],
	[Url],
	[SequenceOrder],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID] )
VALUES
( 	@SegmentID,
	@ExternalResourceTypeID,
	@UrlText,
	@Url,
	@SequenceOrder,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID )

SET @SegmentExternalResourceID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.SegmentExternalResourceInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[SegmentExternalResourceID],
		[SegmentID],
		[ExternalResourceTypeID],
		[UrlText],
		[Url],
		[SequenceOrder],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]	
	FROM [dbo].[SegmentExternalResource]
	WHERE
		[SegmentExternalResourceID] = @SegmentExternalResourceID
	
	RETURN -- insert successful
END
GO
