CREATE PROCEDURE [dbo].[NameSourceGNFinderSelectAuto]

@DataSourceID INT

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
WHERE	
	[DataSourceID] = @DataSourceID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.NameSourceGNFinderSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
GO
