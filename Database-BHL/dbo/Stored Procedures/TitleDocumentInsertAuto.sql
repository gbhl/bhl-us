CREATE PROCEDURE dbo.TitleDocumentInsertAuto

@TitleDocumentID INT OUTPUT,
@TitleID INT,
@DocumentTypeID INT,
@Name NVARCHAR(200),
@Url NVARCHAR(200),
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[TitleDocument]
( 	[TitleID],
	[DocumentTypeID],
	[Name],
	[Url],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID] )
VALUES
( 	@TitleID,
	@DocumentTypeID,
	@Name,
	@Url,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID )

SET @TitleDocumentID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.TitleDocumentInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END
GO
