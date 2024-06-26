CREATE PROCEDURE [dbo].[ExportPageName]

AS

BEGIN

SET NOCOUNT ON

DECLARE @NameBankID int
SELECT	@NameBankID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'NameBank'

SELECT	ni.IdentifierValue AS NameBankID, 
		CASE WHEN nr.CanonicalNameString = '' THEN nr.ResolvedNameString ELSE nr.CanonicalNameString END AS NameConfirmed, 
		np.PageID, 
		MAX(c.HasLocalContent) AS HasLocalContent,
		MAX(c.HasExternalContent) AS HasExternalContent,
		CONVERT(nvarchar(16), MIN(np.CreationDate), 120) AS CreationDate
FROM	dbo.NamePage np 
		INNER JOIN dbo.Name n ON np.NameID = n.NameID
		INNER JOIN dbo.NameResolved nr ON n.NameResolvedID = nr.NameResolvedID
		LEFT JOIN dbo.NameIdentifier ni ON nr.NameResolvedID = ni.NameResolvedID AND ni.IdentifierID = @NameBankID
		INNER JOIN dbo.Page p ON np.PageID = p.PageID
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		INNER JOIN dbo.Book b ON ip.ItemID = b.ItemID
		INNER JOIN dbo.Item i ON b.ItemID = i.ItemID
		INNER JOIN dbo.SearchCatalog c ON b.BookID = c.ItemID
WHERE	p.Active = 1
AND		(nr.CanonicalNameString <> '' OR nr.ResolvedNameString <> '')
GROUP BY
		ni.IdentifierValue,
		nr.ResolvedNameString,
		nr.CanonicalNameString,
		np.PageID

END

GO
