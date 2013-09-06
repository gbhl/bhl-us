CREATE PROCEDURE dbo.PageSelectExternalUrlForPageID 

@PageID int

AS

BEGIN

SET NOCOUNT ON

DECLARE @BaseURL nvarchar(1000) 
SELECT @BaseURL = ConfigurationValue FROM dbo.Configuration c WHERE ConfigurationName = 'ImageBaseURL'

SELECT	@BaseUrl + AltExternalURL AS AltExternalURL
FROM	dbo.Page p INNER JOIN dbo.Item i ON p.ItemID = i.ItemID
WHERE	p.PageID = @PageID
AND		i.ItemStatusID = 40

END

