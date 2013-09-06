
CREATE PROCEDURE [dbo].[ApiPageSelectByItemID]

@ItemID INT

AS 

SET NOCOUNT ON

DECLARE @RedirItemID int
SELECT @RedirItemID = RedirectItemID FROM dbo.Item WHERE ItemID = @ItemID

IF (@RedirItemID IS NOT NULL)
	exec [dbo].[ApiPageSelectByItemID] @RedirItemID
ELSE
	SELECT	PageID,
			ItemID,
			SequenceOrder,
			[Year],
			Series,
			Volume,
			Issue,
			dbo.fnIndicatedPageStringForPage(PageID) AS PageNumbers,
			dbo.fnPageTypeStringForPage(PageID) AS PageTypeName
	FROM	dbo.Page
	WHERE	ItemID = @ItemID
	AND		Active = 1
	ORDER BY
			SequenceOrder ASC

