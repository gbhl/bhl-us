CREATE PROCEDURE dbo.NameSourceGNFinderUpdateAuto

@DataSourceID INT,
@GNDataSourceName NVARCHAR(200),
@BHLIdentifierID INT,
@GNDataSourceLabel NVARCHAR(200),
@GNDataSourceIcon NVARCHAR(300),
@GNDataSourceURLFormat NVARCHAR(300)

AS 

SET NOCOUNT ON

UPDATE [dbo].[NameSourceGNFinder]
SET
	[DataSourceID] = @DataSourceID,
	[GNDataSourceName] = @GNDataSourceName,
	[BHLIdentifierID] = @BHLIdentifierID,
	[GNDataSourceLabel] = @GNDataSourceLabel,
	[GNDataSourceIcon] = @GNDataSourceIcon,
	[GNDataSourceURLFormat] = @GNDataSourceURLFormat
WHERE
	[DataSourceID] = @DataSourceID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.NameSourceGNFinderUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[DataSourceID],
		[GNDataSourceName],
		[BHLIdentifierID],
		[GNDataSourceLabel],
		[GNDataSourceIcon],
		[GNDataSourceURLFormat]
	FROM [dbo].[NameSourceGNFinder]
	WHERE
		[DataSourceID] = @DataSourceID
	
	RETURN -- update successful
END
GO
