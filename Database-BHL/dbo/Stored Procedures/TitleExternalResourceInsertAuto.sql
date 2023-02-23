CREATE PROCEDURE dbo.TitleExternalResourceInsertAuto

@TitleExternalResourceID INT OUTPUT,
@TitleID INT,
@TitleExternalResourceTypeID INT,
@UrlText NVARCHAR(100),
@Url NVARCHAR(200),
@SequenceOrder SMALLINT,
@CreationUserID INT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[TitleExternalResource]
( 	[TitleID],
	[TitleExternalResourceTypeID],
	[UrlText],
	[Url],
	[SequenceOrder],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID] )
VALUES
( 	@TitleID,
	@TitleExternalResourceTypeID,
	@UrlText,
	@Url,
	@SequenceOrder,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID )

SET @TitleExternalResourceID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.TitleExternalResourceInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END
GO
