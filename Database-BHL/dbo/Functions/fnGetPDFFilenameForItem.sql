CREATE FUNCTION [dbo].[fnGetPDFFilenameForItem]
(
	@ItemID int
)
RETURNS nvarchar(100)
AS 
BEGIN
	DECLARE @RemoteFileName nvarchar(100)

	SET @RemoteFileName = dbo.fnGetRemoteFileName(@ItemID, 'Text PDF')
	IF (@RemoteFileName IS NULL) SET @RemoteFileName = dbo.fnGetRemoteFileName(@ItemID, 'Standard LuraTech PDF')
	IF (@RemoteFileName IS NULL) SET @RemoteFileName = dbo.fnGetRemoteFileName(@ItemID, 'Image Container PDF')
	IF (@RemoteFileName IS NULL) SET @RemoteFileName = dbo.fnGetRemoteFileName(@ItemID, 'PDF')

	RETURN ISNULL(@RemoteFileName, '')
END
