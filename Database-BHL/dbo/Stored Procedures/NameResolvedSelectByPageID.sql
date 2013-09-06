
CREATE PROCEDURE [dbo].[NameResolvedSelectByPageID]

@PageID INT

AS

BEGIN

SET NOCOUNT ON

DECLARE @NameBankID int
DECLARE @EOLID int
SELECT @NameBankID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'NameBank'
SELECT @EOLID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'EOL'

CREATE TABLE #tmpName
	(
	NameResolvedID int,
	ResolvedNameString nvarchar(100),
	NameBankID nvarchar(100),
	EOLID nvarchar(100)
	)

INSERT	#tmpName
SELECT	nr.NameResolvedID,
		nr.ResolvedNameString,
		NULL AS NameBankID,
		NULL AS EOLID
FROM	dbo.NamePage np 
		INNER JOIN dbo.Name n ON np.NameID = n.NameID
		INNER JOIN dbo.NameResolved nr ON n.NameResolvedID = nr.NameResolvedID
WHERE	np.PageID = @PageID
AND		n.IsActive = 1
GROUP BY
		nr.NameResolvedID,
		nr.ResolvedNameString

-- For performance reasons, add the NameBankID and EOLID after the initial select
UPDATE	#tmpName
SET		NameBankID = ni.IdentifierValue
FROM	#tmpName t INNER JOIN dbo.NameIdentifier ni ON t.NameResolvedID = ni.NameResolvedID AND ni.IdentifierID = @NameBankID

UPDATE	#tmpName
SET		EOLID = ni.IdentifierValue
FROM	#tmpName t INNER JOIN dbo.NameIdentifier ni ON t.NameResolvedID = ni.NameResolvedID AND ni.IdentifierID = @EOLID

SELECT	NameResolvedID,
		ResolvedNameString,
		NameBankID,
		ISNULL(CONVERT(nvarchar(100), EOLID), '') AS EOLID
FROM	#tmpName
ORDER BY ResolvedNameString

END


