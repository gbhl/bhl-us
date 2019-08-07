CREATE FUNCTION [dbo].[fnGetDjvuFilenameForItem]
(
	@ItemID int
)
RETURNS nvarchar(100)
AS 
BEGIN
	DECLARE @RemoteFileName nvarchar(100)
	SET @RemoteFileName = dbo.fnGetRemoteFileName(@ItemID, 'Djvu XML')
	RETURN ISNULL(@RemoteFileName, '')
END
