
CREATE PROCEDURE [dbo].[ApiNameResolvedSelectByNameLike]

@NameConfirmed NVARCHAR(100),
@ReturnCount INT = 100

AS 

SET NOCOUNT ON

-- Add these variables to "re-write" the inputs because the SQL optimizer 
-- kept creating bad plans for this procedure
DECLARE @Name nvarchar(100)
DECLARE @Count int
	
SET @Name = @NameConfirmed
SET @Count = @ReturnCount


DECLARE @NameBank int
SELECT @NameBank = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'NameBank';

CREATE TABLE #tmpName
	(
	NameResolvedID int,
	ResolvedNameString nvarchar(100),
	NameBankID nvarchar(100)
	)

INSERT	#tmpName
SELECT TOP (@Count)
		nr.NameResolvedID,
		nr.ResolvedNameString,
		NULL AS NameBankID
FROM	dbo.NameResolved nr WITH (NOLOCK)
		INNER JOIN dbo.Name n WITH (NOLOCK) ON nr.NameResolvedID = n.NameResolvedID
WHERE 	nr.ResolvedNameString LIKE LTRIM(RTRIM(@Name)) + '%'
AND		n.IsActive = 1
GROUP BY nr.NameResolvedID, nr.ResolvedNameString
ORDER BY nr.ResolvedNameString ASC

-- For performance reasons, add the NameBankID after the initial select
UPDATE	#tmpName
SET		NameBankID = ni.IdentifierValue
FROM	#tmpName t INNER JOIN dbo.NameIdentifier ni WITH (NOLOCK) ON t.NameResolvedID = ni.NameResolvedID AND ni.IdentifierID = @NameBank

SELECT	ResolvedNameString,
		NameBankID
FROM	#tmpName
ORDER BY ResolvedNameString ASC


