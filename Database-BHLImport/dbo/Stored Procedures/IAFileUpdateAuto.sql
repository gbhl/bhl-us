
-- IAFileUpdateAuto PROCEDURE
-- Generated 8/23/2010 3:08:23 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for IAFile

CREATE PROCEDURE IAFileUpdateAuto

@FileID INT,
@ItemID INT,
@RemoteFileName NVARCHAR(100),
@LocalFileName NVARCHAR(100),
@Source NVARCHAR(20),
@Format NVARCHAR(50),
@Original NVARCHAR(50),
@RemoteFileLastModifiedDate DATETIME

AS 

SET NOCOUNT ON

UPDATE [dbo].[IAFile]

SET

	[ItemID] = @ItemID,
	[RemoteFileName] = @RemoteFileName,
	[LocalFileName] = @LocalFileName,
	[Source] = @Source,
	[Format] = @Format,
	[Original] = @Original,
	[RemoteFileLastModifiedDate] = @RemoteFileLastModifiedDate,
	[LastModifiedDate] = getdate()

WHERE
	[FileID] = @FileID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAFileUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

