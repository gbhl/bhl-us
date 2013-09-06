
CREATE PROCEDURE [dbo].[NamePageSelectByPageIDAndNameString]

@PageID INT,
@NameString NVARCHAR(100)

AS 

SET NOCOUNT ON

DECLARE @NameBankID int
SELECT @NameBankID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'NameBank'

SELECT	np.NamePageID,
		np.NameID,
		np.PageID,
		ns.SourceName,
		n.NameString,
		nr.ResolvedNameString,
		ni.IdentifierValue AS NameBankID,
		n.IsActive,
		np.CreationDate,
		np.LastModifiedDate,
		np.CreationUserID,
		np.LastModifiedUserID
FROM	dbo.NamePage np 
		INNER JOIN dbo.Name n ON np.NameID = n.NameID
		INNER JOIN dbo.NameSource ns ON np.NameSourceID = ns.NameSourceID
		LEFT JOIN dbo.NameResolved nr ON n.NameResolvedID = nr.NameResolvedID
		LEFT JOIN dbo.NameIdentifier ni ON nr.NameResolvedID = ni.NameResolvedID AND ni.IdentifierID = @NameBankID
WHERE	np.PageID = @PageID 
AND		n.NameString = @NameString



