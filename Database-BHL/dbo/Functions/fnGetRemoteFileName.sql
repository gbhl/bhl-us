SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fnGetRemoteFileName]
(
	@ItemID int,
	@Format NVARCHAR(50)
)
RETURNS NVARCHAR(100)
AS
BEGIN
	DECLARE @RemoteFileName nvarchar(100)

	SELECT	@RemoteFileName = MIN(ISNULL(f1.RemoteFileName, f2.RemoteFileName))
	FROM	dbo.Item i
			LEFT JOIN dbo.Book b ON i.ItemID = b.ItemID
			LEFT JOIN dbo.BHLImportIAItem ii1 ON b.Barcode = ii1.IAIdentifier
			LEFT JOIN dbo.BHLImportIAFile f1 ON ii1.ItemID = f1.ItemID AND f1.[Format] = @Format
			LEFT JOIN dbo.Segment s ON i.ItemID = s.ItemID
			LEFT JOIN dbo.BHLImportIAItem ii2 ON s.Barcode = ii2.IAIdentifier
			LEFT JOIN dbo.BHLImportIAFile f2 ON ii2.ItemID = f2.ItemID AND f2.[Format] = @Format
	WHERE	i.ItemID = @ItemID
	
	RETURN @RemoteFileName
END



GO
