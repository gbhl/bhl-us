
CREATE PROCEDURE [dbo].[IASetSelectForDownload]

AS 

SET NOCOUNT ON

SELECT 

	[SetID],
	[SetSpecification],
	[DownloadAll],
	[LastDownloadDate],
	[LastFullDownloadDate],
	[CreatedDate],
	[LastModifiedDate]

FROM [dbo].[IASet]

WHERE
	[DownloadAll] = 1

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IASetSelectForDownload. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
