CREATE PROCEDURE [dbo].[PageFlickrSelectRandom]

@NumToReturn int = 10

AS

BEGIN

SET NOCOUNT ON;

WITH CTE AS (
	SELECT TOP (@NumToReturn) 
			f.PageID, 
			f.FlickrURL
	FROM	dbo.PageFlickr f
	ORDER BY NEWID()
)
SELECT DISTINCT
		cte.PageID, 
		cte.FlickrURL, 
		v.ShortTitle, 
		dbo.fnIndicatedPageStringForPage(cte.PageID) AS IndicatedPage,
		dbo.fnPageTypeStringForPage(cte.PageID) AS PageType
FROM	cte INNER JOIN dbo.PageSummaryView v
			ON cte.PageID = v.PageID

END
