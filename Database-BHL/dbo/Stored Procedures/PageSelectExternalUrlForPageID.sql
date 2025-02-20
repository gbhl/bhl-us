CREATE PROCEDURE [dbo].[PageSelectExternalUrlForPageID] 

@PageID int

AS

BEGIN

SET NOCOUNT ON

DECLARE @BaseURL nvarchar(1000) 
SELECT @BaseURL = ConfigurationValue FROM dbo.Configuration c WHERE ConfigurationName = 'ImageBaseURL'

SELECT DISTINCT
		CASE WHEN CHARINDEX('archive.org', @BaseURL) > 0 
		THEN @BaseUrl + p.ExternalURL 
		ELSE @BaseURL + '/web/' + b.Barcode + '/' + b.Barcode + '_' + RIGHT('0000' + CONVERT(varchar(4), ip.SequenceOrder), 4) + '.jpg'
		END AS AltExternalURL
FROM	dbo.Page p 
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		INNER JOIN dbo.Item i ON ip.ItemID = i.ItemID
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
WHERE	p.PageID = @PageID
AND		i.ItemStatusID = 40
UNION
SELECT DISTINCT
		CASE WHEN CHARINDEX('archive.org', @BaseURL) > 0 
		THEN @BaseUrl + p.ExternalURL 
		ELSE @BaseURL + '/web/' + s.Barcode + '/' + s.Barcode + '_' + RIGHT('0000' + CONVERT(varchar(4), ip.SequenceOrder), 4) + '.jpg'
		END AS AltExternalURL
FROM	dbo.Page p 
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		INNER JOIN dbo.Item i ON ip.ItemID = i.ItemID
		INNER JOIN dbo.vwSegment s ON i.ItemID = s.ItemID
		INNER JOIN dbo.Book b ON s.BookID = b.BookID AND b.IsVirtual = 1
WHERE	p.PageID = @PageID
AND		i.ItemStatusID IN (30, 40)


END

GO
