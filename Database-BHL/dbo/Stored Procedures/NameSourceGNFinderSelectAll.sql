CREATE PROCEDURE [dbo].[NameSourceGNFinderSelectAll]

AS 

SET NOCOUNT ON

SELECT	
	[DataSourceID],
	[GNDataSourceName],
	[BHLIdentifierID],
	[GNDataSourceLabel],
	[GNDataSourceIcon],
	[GNDataSourceURLFormat]
FROM	
	[dbo].[NameSourceGNFinder]

GO
