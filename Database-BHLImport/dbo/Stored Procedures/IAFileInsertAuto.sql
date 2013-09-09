
-- IAFileInsertAuto PROCEDURE
-- Generated 8/23/2010 3:08:23 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for IAFile

CREATE PROCEDURE IAFileInsertAuto

@FileID INT OUTPUT,
@ItemID INT,
@RemoteFileName NVARCHAR(100),
@LocalFileName NVARCHAR(100),
@Source NVARCHAR(20),
@Format NVARCHAR(50),
@Original NVARCHAR(50),
@RemoteFileLastModifiedDate DATETIME = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[IAFile]
(
	[ItemID],
	[RemoteFileName],
	[LocalFileName],
	[Source],
	[Format],
	[Original],
	[RemoteFileLastModifiedDate],
	[CreatedDate],
	[LastModifiedDate]
)
VALUES
(
	@ItemID,
	@RemoteFileName,
	@LocalFileName,
	@Source,
	@Format,
	@Original,
	@RemoteFileLastModifiedDate,
	getdate(),
	getdate()
)

SET @FileID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAFileInsertAuto. No information was inserted as a result of this request.', 16, 1)
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

