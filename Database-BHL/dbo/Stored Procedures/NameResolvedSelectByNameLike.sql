
CREATE PROCEDURE [dbo].[NameResolvedSelectByNameLike]

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
	NameBankID nvarchar(100),
	PageCount int
	)

INSERT	#tmpName
SELECT TOP (@Count)
		nr.NameResolvedID,
		nr.ResolvedNameString,
		NULL AS NameBankID,
		COUNT(np.NamePageID) AS [PageCount]
FROM	dbo.NameResolved nr WITH (NOLOCK)
		INNER JOIN dbo.Name n WITH (NOLOCK) ON nr.NameResolvedID = n.NameResolvedID
		INNER JOIN dbo.NamePage np WITH (NOLOCK) ON n.NameID = np.NameID
WHERE 	nr.ResolvedNameString LIKE LTRIM(RTRIM(@Name)) + '%'
AND		n.IsActive = 1
GROUP BY nr.NameResolvedID, nr.ResolvedNameString

-- For performance reasons, add the NameBankID after the initial select
UPDATE	#tmpName
SET		NameBankID = ni.IdentifierValue
FROM	#tmpName t INNER JOIN dbo.NameIdentifier ni WITH (NOLOCK) ON t.NameResolvedID = ni.NameResolvedID AND ni.IdentifierID = @NameBank

SELECT	ResolvedNameString,
		NameBankID,
		PageCount
FROM	#tmpName
ORDER BY ResolvedNameString ASC



