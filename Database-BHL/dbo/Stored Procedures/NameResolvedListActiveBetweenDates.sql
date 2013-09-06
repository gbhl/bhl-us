
CREATE PROCEDURE [dbo].[NameResolvedListActiveBetweenDates]

@StartRow INT,
@BatchSize INT,
@StartDate DATETIME,
@EndDate DATETIME

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
FROM	dbo.NamePage pn WITH (NOLOCK)
		INNER JOIN dbo.Name n WITH (NOLOCK) ON pn.NameID = n.NameID
		INNER JOIN dbo.NameResolved nr WITH (NOLOCK) ON n.NameResolvedID = nr.NameResolvedID
		INNER JOIN dbo.NameIdentifier ni WITH (NOLOCK) ON nr.NameResolvedID = ni.NameResolvedID AND ni.IdentifierID = @NameBank
WHERE	n.IsActive = 1
AND		(pn.CreationDate BETWEEN @StartDate AND @EndDate OR
		pn.LastModifiedDate BETWEEN @StartDate AND @EndDate)
GROUP BY
		nr.ResolvedNameString,
		ni.IdentifierValue
)
SELECT	ResolvedNameString, NameBankID
FROM	Names
WHERE	RowNumber BETWEEN @StartRow AND @EndRow

