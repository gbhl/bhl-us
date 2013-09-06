
CREATE PROCEDURE [dbo].[NamePageSelectByPageID]

@PageID INT

AS 

SET NOCOUNT ON

DECLARE @NameBankID int
DECLARE @EOLID int
SELECT @NameBankID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'NameBank'
SELECT @EOLID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'EOL'

SELECT	np.NamePageID,
		np.NameID,
		np.PageID,
		np.NameSourceID,
		ns.SourceName,
		n.NameString,
		nr.ResolvedNameString,
		ni1.IdentifierValue AS NameBankID,
		ISNULL(MIN(ni2.IdentifierValue), '') AS EOLID,
		n.IsActive,
		np.IsFirstOccurrence,
		np.CreationDate,
		np.LastModifiedDate,
		np.CreationUserID,
		np.LastModifiedUserID
FROM	dbo.NamePage np 
		INNER JOIN dbo.Name n ON np.NameID = n.NameID
		INNER JOIN dbo.NameSource ns ON np.NameSourceID = ns.NameSourceID
		LEFT JOIN dbo.NameResolved nr ON n.NameResolvedID = nr.NameResolvedID
		LEFT JOIN dbo.NameIdentifier ni1 ON nr.NameResolvedID = ni1.NameResolvedID AND ni1.IdentifierID = @NameBankID
		LEFT JOIN dbo.NameIdentifier ni2 ON nr.NameResolvedID = ni2.NameResolvedID AND ni2.IdentifierID = @EOLID
WHERE	np.PageID = @PageID
GROUP BY
		np.NamePageID,
		np.NameID,
		np.PageID,
		np.NameSourceID,
		ns.SourceName,
		n.NameString,
		nr.ResolvedNameString,
		ni1.IdentifierValue,
		n.IsActive,
		np.IsFirstOccurrence,
		np.CreationDate,
		np.LastModifiedDate,
		np.CreationUserID,
		np.LastModifiedUserID
ORDER BY n.NameString

