CREATE FUNCTION [dbo].[fnGetTextFilenameForItem]
(
	@ItemID int
)
RETURNS nvarchar(100)
AS 
BEGIN
	DECLARE @RemoteFileName nvarchar(100)
	SET @RemoteFileName = dbo.fnGetRemoteFileName(@ItemID, 'DjVuTXT')
	RETURN ISNULL(@RemoteFileName, '')
END
