CREATE PROCEDURE [dbo].[PageFlickrSelectRandom]

@NumToReturn int = 10

AS

BEGIN

SET NOCOUNT ON

SELECT TOP (@NumToReturn)
		f.PageID, 
		f.FlickrURL, 
		v.ShortTitle, 
		dbo.fnIndicatedPageStringForPage(f.PageID) AS IndicatedPage,
		dbo.fnPageTypeStringForPage(f.PageID) AS PageType
FROM	dbo.PageFlickr f INNER JOIN dbo.PageSummaryView v
			ON f.PageID = v.PageID
-- Default set
--WHERE	f.PageID IN (1203991, 10535931, 3894920, 8448711, 306998, 7360249, 12042246, 6307518, 12232608, 1203798)
ORDER BY NEWID()

END


