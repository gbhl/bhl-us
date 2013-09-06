CREATE PROCEDURE [dbo].[IndicatedPageSelectByPageID]

@PageID INT

AS 

SET NOCOUNT ON

SELECT	Sequence,
		PagePrefix,
		PageNumber,
		Implied
FROM	dbo.IndicatedPage
WHERE	PageID = @PageID
ORDER BY
		Sequence ASC

