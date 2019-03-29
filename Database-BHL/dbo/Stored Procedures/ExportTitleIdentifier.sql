CREATE PROCEDURE [dbo].[ExportTitleIdentifier]

AS

BEGIN

SET NOCOUNT ON

SELECT 	tti.TitleID, 
		ti.IdentifierName, 
		tti.IdentifierValue, 
		MAX(c.HasLocalContent) AS HasLocalContent,
		MAX(c.HasExternalContent) AS HasExternalContent,
		CONVERT(nvarchar(16), tti.CreationDate, 120) AS CreationDate
FROM	dbo.Title t WITH (NOLOCK)
		INNER JOIN dbo.Title_Identifier tti WITH (NOLOCK) ON t.TitleID = tti.TitleID
		INNER JOIN dbo.Identifier ti WITH (NOLOCK) ON tti.IdentifierID = ti.IdentifierID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON t.TitleID = i.PrimaryTitleID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID AND i.ItemID = c.ItemID
WHERE	t.PublishReady = 1
GROUP BY
		tti.TitleID, 
		ti.IdentifierName, 
		tti.IdentifierValue, 
		tti.CreationDate

END
