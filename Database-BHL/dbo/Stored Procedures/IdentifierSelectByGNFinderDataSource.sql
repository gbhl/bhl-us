CREATE PROCEDURE dbo.IdentifierSelectByGNFinderDataSource

@DataSourceID int

AS

BEGIN

SET NOCOUNT ON

SELECT	i.*
FROM	dbo.NameSourceGNFinder g
		INNER JOIN dbo.Identifier i ON g.BHLIdentifierID = i.IdentifierID
WHERE	g.DataSourceID = @DataSourceID

END
