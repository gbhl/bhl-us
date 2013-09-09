CREATE PROCEDURE [dbo].[IAFileSelectByItemAndFormat]

@ItemID INT,
@Format NVARCHAR(50)

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
	[ItemID] = @ItemID
AND	[Format] = @Format

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAFileSelectByItemAndFormat. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END



