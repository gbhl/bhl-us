CREATE PROCEDURE dbo.SegmentExternalResourceUpdateAuto

@SegmentExternalResourceID INT,
@SegmentID INT,
@ExternalResourceTypeID INT,
@UrlText NVARCHAR(100),
@Url NVARCHAR(200),
@SequenceOrder SMALLINT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[SegmentExternalResource]
SET
	[SegmentID] = @SegmentID,
	[ExternalResourceTypeID] = @ExternalResourceTypeID,
	[UrlText] = @UrlText,
	[Url] = @Url,
	[SequenceOrder] = @SequenceOrder,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID
WHERE
	[SegmentExternalResourceID] = @SegmentExternalResourceID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.SegmentExternalResourceUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END
GO
