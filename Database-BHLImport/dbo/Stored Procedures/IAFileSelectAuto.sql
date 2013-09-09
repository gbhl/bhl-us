
-- IAFileSelectAuto PROCEDURE
-- Generated 8/23/2010 3:08:23 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for IAFile

CREATE PROCEDURE IAFileSelectAuto

@FileID INT

AS 

SET NOCOUNT ON

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

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAFileSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

