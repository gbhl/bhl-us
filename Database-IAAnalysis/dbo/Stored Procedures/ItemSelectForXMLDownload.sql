CREATE PROCEDURE [dbo].[ItemSelectForXMLDownload]

AS
SET NOCOUNT ON

SELECT	ItemID,
		Identifier,
		MetaGetStatus,
		MarcGetStatus
FROM	dbo.Item
WHERE	ItemStatusID = 10
AND		MetaGetStatus <> 'NotFound'
AND		MarcGetStatus <> 'NotFound'
-- Disregard items more than 30 days old, as they are unlikely to be "fixed".
AND		CreationDate > GETDATE() - 30

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemSelectForXMLDownload. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

