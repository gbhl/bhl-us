CREATE FUNCTION [dbo].[fnGetRemoteFileName]
(
	@ItemID int,
	@Format NVARCHAR(50)
)
RETURNS NVARCHAR(100)
AS
BEGIN
	DECLARE @RemoteFileName nvarchar(100)

	SELECT	@RemoteFileName = MIN(f.RemoteFileName)
	FROM	dbo.Item bi
			INNER JOIN dbo.BHLImportIAItem ii ON bi.Barcode = ii.IAIdentifier
			INNER JOIN dbo.BHLImportIAFile f ON ii.ItemID = f.ItemID AND f.[Format] = @Format
	WHERE	bi.ItemID = @ItemID
	
	RETURN @RemoteFileName
END
