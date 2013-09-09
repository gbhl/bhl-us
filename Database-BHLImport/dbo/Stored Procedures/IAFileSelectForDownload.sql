CREATE PROCEDURE [dbo].[IAFileSelectForDownload]

@ItemID INT

AS 

SET NOCOUNT ON

SELECT 
	f.[FileID],
	f.[ItemID],
	f.[RemoteFileName],
	f.[LocalFileName],
	f.[Source],
	f.[Format],
	f.[Original],
	f.[RemoteFileLastModifiedDate],
	f.[CreatedDate],
	f.[LastModifiedDate]
FROM [dbo].[IAFile] f INNER JOIN [dbo].[IAFileFormat] ff
		ON f.[Format] = ff.[Format]
WHERE
	ff.[Download] = 1
AND	f.[ItemID] = @ItemID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAFileSelectForDownload. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END



