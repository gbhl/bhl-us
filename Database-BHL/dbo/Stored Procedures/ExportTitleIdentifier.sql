CREATE PROCEDURE dbo.ExportTitleIdentifier

AS

BEGIN

SET NOCOUNT ON

SELECT DISTINCT 
		tti.TitleID, 
		ti.IdentifierName, 
		tti.IdentifierValue, 
		CONVERT(nvarchar(16), tti.CreationDate, 120) AS CreationDate
FROM	dbo.Title t WITH (NOLOCK)
		INNER JOIN dbo.Title_Identifier tti WITH (NOLOCK) ON t.TitleID = tti.TitleID
		INNER JOIN dbo.Identifier ti WITH (NOLOCK) ON tti.IdentifierID = ti.IdentifierID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON t.TitleID = i.PrimaryTitleID
WHERE	t.PublishReady = 1

END

