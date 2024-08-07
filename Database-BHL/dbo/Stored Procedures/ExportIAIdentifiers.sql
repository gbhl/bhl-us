SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ExportIAIdentifiers]

AS

BEGIN

SET NOCOUNT ON

-- Export the list of barcodes (identifiers) for all
-- active items contributed via Internet Archive
SELECT	b.BarCode
FROM	dbo.Book b
		INNER JOIN dbo.Item i ON b.ItemID = i.ItemID
WHERE	i.ItemSourceID = 1	-- Internet Archive
AND		i.ItemStatusID = 40	-- Published
AND		b.IsVirtual = 0
		
UNION

SELECT	s.BarCode
FROM	dbo.Segment s
		INNER JOIN dbo.Item i ON s.ItemID = i.ItemID
WHERE	i.ItemSourceID = 1 -- Internet Archive
AND		i.ItemStatusID IN (30, 40) -- New, Published 

ORDER BY
		b.BarCode

END

GO
