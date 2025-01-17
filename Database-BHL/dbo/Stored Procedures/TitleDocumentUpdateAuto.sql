CREATE PROCEDURE dbo.TitleDocumentUpdateAuto

@TitleDocumentID INT,
@TitleID INT,
@DocumentTypeID INT,
@Name NVARCHAR(200),
@Url NVARCHAR(200),
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[TitleDocument]
SET
	[TitleID] = @TitleID,
	[DocumentTypeID] = @DocumentTypeID,
	[Name] = @Name,
	[Url] = @Url,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID
WHERE
	[TitleDocumentID] = @TitleDocumentID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.TitleDocumentUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[TitleDocumentID],
		[TitleID],
		[DocumentTypeID],
		[Name],
		[Url],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]
	FROM [dbo].[TitleDocument]
	WHERE
		[TitleDocumentID] = @TitleDocumentID
	
	RETURN -- update successful
END
GO
