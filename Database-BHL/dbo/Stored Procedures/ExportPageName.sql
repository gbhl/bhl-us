CREATE PROCEDURE dbo.ExportPageName

AS

BEGIN

SET NOCOUNT ON

SELECT	ni.IdentifierValue AS NameBankID, 
		nr.ResolvedNameString AS NameConfirmed, 
		np.PageID, 
		CONVERT(nvarchar(16), np.CreationDate, 120) AS CreationDate
FROM	dbo.NamePage np WITH (NOLOCK)
		INNER JOIN dbo.Name n WITH (NOLOCK) ON np.NameID = n.NameID
		INNER JOIN dbo.NameResolved nr WITH (NOLOCK) ON n.NameResolvedID = nr.NameResolvedID
		LEFT JOIN dbo.NameIdentifier ni WITH (NOLOCK) ON nr.NameResolvedID = ni.NameResolvedID AND ni.IdentifierID = 17

END
