CREATE PROCEDURE dbo.TitleExternalResourceUpdateAuto

@TitleExternalResourceID INT,
@TitleID INT,
@TitleExternalResourceTypeID INT,
@UrlText NVARCHAR(100),
@Url NVARCHAR(200),
@SequenceOrder SMALLINT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[TitleExternalResource]
SET
	[TitleID] = @TitleID,
	[TitleExternalResourceTypeID] = @TitleExternalResourceTypeID,
	[UrlText] = @UrlText,
	[Url] = @Url,
	[SequenceOrder] = @SequenceOrder,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID
WHERE
	[TitleExternalResourceID] = @TitleExternalResourceID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.TitleExternalResourceUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[TitleExternalResourceID],
		[TitleID],
		[TitleExternalResourceTypeID],
		[UrlText],
		[Url],
		[SequenceOrder],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]
	FROM [dbo].[TitleExternalResource]
	WHERE
		[TitleExternalResourceID] = @TitleExternalResourceID
	
	RETURN -- update successful
END
GO
