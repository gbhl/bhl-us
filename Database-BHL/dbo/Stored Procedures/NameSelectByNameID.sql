
CREATE PROCEDURE [dbo].[NameSelectByNameID]

@NameID int

AS

BEGIN

SET NOCOUNT ON

DECLARE @NameBankID int
DECLARE @EOLID int

SELECT @NameBankID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'NameBank'
SELECT @EOLID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'EOL'


SELECT	n.NameID,
		n.NameSourceID,
		n.NameString,
		nr.ResolvedNameString,
		nr.CanonicalNameString,
		ni1.IdentifierValue AS NamebankID,
		ni2.IdentifierValue AS EOLID,
		n.IsActive,
		n.CreationDate,
		n.LastModifiedDate,
		n.CreationUserID,
		n.LastModifiedUserID
FROM	dbo.Name n LEFT JOIN dbo.NameResolved nr ON n.NameResolvedID = nr.NameResolvedID
		LEFT JOIN dbo.NameIdentifier ni1 ON nr.NameResolvedID = ni1.NameResolvedID AND ni1.IdentifierID = @NameBankID
		LEFT JOIN dbo.NameIdentifier ni2 ON nr.NameResolvedID = ni2.NameResolvedID AND ni2.IdentifierID = @EOLID
WHERE	n.NameID = @NameID

END


