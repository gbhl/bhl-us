CREATE PROCEDURE dbo.IAFileInsertAuto

@FileID INT OUTPUT,
@ItemID INT,
@RemoteFileName NVARCHAR(250),
@LocalFileName NVARCHAR(250),
@Source NVARCHAR(20),
@Format NVARCHAR(50),
@Original NVARCHAR(50),
@RemoteFileLastModifiedDate DATETIME = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[IAFile]
( 	[ItemID],
	[RemoteFileName],
	[LocalFileName],
	[Source],
	[Format],
	[Original],
	[RemoteFileLastModifiedDate],
	[CreatedDate],
	[LastModifiedDate] )
VALUES
( 	@ItemID,
	@RemoteFileName,
	@LocalFileName,
	@Source,
	@Format,
	@Original,
	@RemoteFileLastModifiedDate,
	getdate(),
	getdate() )

SET @FileID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.IAFileInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[FileID],
		[ItemID],
		[RemoteFileName],
		[LocalFileName],
		[Source],
		[Format],
		[Original],
		[RemoteFileLastModifiedDate],
		[CreatedDate],
		[LastModifiedDate]	
	FROM [dbo].[IAFile]
	WHERE
		[FileID] = @FileID
	
	RETURN -- insert successful
END
GO
