SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PageTextLogSelectForItem]

@ItemID int

AS

BEGIN

SET NOCOUNT ON

SELECT	[PageTextLogID],
		l.[PageID],
		[TextSource],
		[TextImportBatchFileID],
		l.[CreationDate],
		l.[CreationUserID]
FROM	dbo.PageTextLog l
		INNER JOIN dbo.Page p ON l.PageID = p.PageID
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
WHERE	ip.ItemID = @ItemID

END

GO
