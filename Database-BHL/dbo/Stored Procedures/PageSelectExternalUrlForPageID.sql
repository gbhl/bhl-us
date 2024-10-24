SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PageSelectExternalUrlForPageID] 

@PageID int

AS

BEGIN

SET NOCOUNT ON

DECLARE @BaseURL nvarchar(1000) 
SELECT @BaseURL = ConfigurationValue FROM dbo.Configuration c WHERE ConfigurationName = 'ImageBaseURL'

SELECT	@BaseUrl + ExternalURL AS AltExternalURL
FROM	dbo.Page p 
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		INNER JOIN dbo.Item i ON ip.ItemID = i.ItemID
WHERE	p.PageID = @PageID
AND		i.ItemStatusID = 40

END


GO
