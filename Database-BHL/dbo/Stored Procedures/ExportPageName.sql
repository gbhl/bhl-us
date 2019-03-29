CREATE PROCEDURE [dbo].[ExportPageName]

AS

BEGIN

SET NOCOUNT ON

SELECT DISTINCT
		ni.IdentifierValue AS NameBankID, 
		nr.ResolvedNameString AS NameConfirmed, 
		np.PageID, 
		c.HasLocalContent,
		c.HasExternalContent,
		CONVERT(nvarchar(16), np.CreationDate, 120) AS CreationDate
FROM	dbo.NamePage np WITH (NOLOCK)
		INNER JOIN dbo.Name n WITH (NOLOCK) ON np.NameID = n.NameID
		INNER JOIN dbo.NameResolved nr WITH (NOLOCK) ON n.NameResolvedID = nr.NameResolvedID
		LEFT JOIN dbo.NameIdentifier ni WITH (NOLOCK) ON nr.NameResolvedID = ni.NameResolvedID AND ni.IdentifierID = 17
		INNER JOIN dbo.Page p WITH (NOLOCK) ON np.PageID = p.PageID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON p.ItemID = c.ItemID

END
