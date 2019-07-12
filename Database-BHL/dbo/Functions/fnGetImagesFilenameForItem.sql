CREATE FUNCTION [dbo].[fnGetImagesFilenameForItem]
(
	@ItemID int
)
RETURNS nvarchar(100)
AS 
BEGIN
	DECLARE @RemoteFileName nvarchar(100)

	SET @RemoteFileName = dbo.fnGetRemoteFileName(@ItemID, 'Single Page Processed JP2 ZIP')
	IF (@RemoteFileName IS NULL) SET @RemoteFileName = dbo.fnGetRemoteFileName(@ItemID, 'Single Page Processed JP2 TAR')
	IF (@RemoteFileName IS NULL) SET @RemoteFileName = dbo.fnGetRemoteFileName(@ItemID, 'Single Page Processed TIFF ZIP')
	IF (@RemoteFileName IS NULL) SET @RemoteFileName = dbo.fnGetRemoteFileName(@ItemID, 'Single Page Processed TIFF TAR')
	IF (@RemoteFileName IS NULL) SET @RemoteFileName = dbo.fnGetRemoteFileName(@ItemID, 'Single Page Processed JPEG ZIP')
	IF (@RemoteFileName IS NULL) SET @RemoteFileName = dbo.fnGetRemoteFileName(@ItemID, 'Single Page Processed JPEG TAR')

	RETURN ISNULL(@RemoteFileName, '')
END
