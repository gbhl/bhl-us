
CREATE PROCEDURE [annotation].[AnnotatedPageSelectByPageID]

@PageID int

AS

BEGIN

SET NOCOUNT ON

SELECT	ap.AnnotatedPageID,
		ap.AnnotatedItemID,
		ap.PageID,
		ap.ExternalIdentifier,
		ap.AnnotatedPageTypeID,
		--ISNULL(ip.PageNumber, ap.PageNumber) AS PageNumber,
		ISNULL(ip.PageNumber, '') AS PageNumber,
		ap.CreationDate,
		ap.LastModifiedDate
FROM	annotation.AnnotatedPage ap LEFT JOIN dbo.IndicatedPage ip
			ON ap.PageID = ip.PageID
			AND ip.Sequence = 1
WHERE	ap.PageID = @PageID

END



