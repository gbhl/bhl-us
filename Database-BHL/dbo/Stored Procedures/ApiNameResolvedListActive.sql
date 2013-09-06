
CREATE PROCEDURE [dbo].[ApiNameResolvedListActive]

@StartRow INT,
@BatchSize INT

AS 

SET NOCOUNT ON

-- Get the specified names
DECLARE @EndRow int
SET @EndRow = @StartRow + @BatchSize - 1;

DECLARE @NameBank int
SELECT @NameBank = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'NameBank';

WITH Names AS
(
SELECT	nr.ResolvedNameString,
		ni.IdentifierValue AS NameBankID,
		ROW_NUMBER() OVER (ORDER BY nr.ResolvedNameString, ni.IdentifierValue) AS RowNumber
FROM	dbo.NameResolved nr WITH (NOLOCK)
		LEFT JOIN dbo.NameIdentifier ni WITH (NOLOCK) ON nr.NameResolvedID = ni.NameResolvedID AND ni.IdentifierID = @NameBank
GROUP BY
		nr.ResolvedNameString,
		ni.IdentifierValue
)
SELECT	ResolvedNameString, NameBankID
FROM	Names
WHERE	RowNumber BETWEEN @StartRow AND @EndRow

