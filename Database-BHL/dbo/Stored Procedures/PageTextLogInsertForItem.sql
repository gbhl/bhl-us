SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PageTextLogInsertForItem]

@ItemID int,
@TextSource nvarchar(50),
@UserID int

AS

BEGIN

SET NOCOUNT ON

INSERT	dbo.PageTextLog (PageID, TextSource, CreationUserID)
SELECT	p.PageID,
		@TextSource,
		@UserID
FROM	dbo.Page p
		INNER JOIN dbo.ItemPage ip ON P.PageID = ip.PageID
WHERE	p.Active = 1
AND		ip.ItemID = @ItemID

END

GO
