CREATE PROCEDURE dbo.ItemSourceSelectAll
AS
BEGIN

SET NOCOUNT ON

SELECT	ItemSourceID,
		SourceName,
		DownloadUrl,
		ImageServerUrlFormat,
		CreationDate,
		LastModifiedDate
FROM	dbo.ItemSource

END
