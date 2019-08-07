CREATE FUNCTION [dbo].[fnGetScandataFilenameForItem]
(
	@ItemID int
)
RETURNS nvarchar(100)
AS 
BEGIN
	DECLARE @RemoteFileName nvarchar(100)

	SET @RemoteFileName = dbo.fnGetRemoteFileName(@ItemID, 'Scandata')
	IF (@RemoteFileName IS NULL) SET @RemoteFileName = dbo.fnGetRemoteFileName(@ItemID, 'Metadata from Scandata ZIP')
	IF (@RemoteFileName IS NULL) SET @RemoteFileName = dbo.fnGetRemoteFileName(@ItemID, 'Microfilm Scandata XML')
	IF (@RemoteFileName IS NULL) SET @RemoteFileName = dbo.fnGetRemoteFileName(@ItemID, 'Scribe Scandata XML')
	IF (@RemoteFileName IS NULL) SET @RemoteFileName = dbo.fnGetRemoteFileName(@ItemID, 'Book Scan Metadata XML')
	
	RETURN ISNULL(@RemoteFileName, '')
END
