CREATE PROCEDURE dbo.NameSourceGNFinderInsertAuto

@DataSourceID INT,
@GNDataSourceName NVARCHAR(200),
@BHLIdentifierID INT,
@GNDataSourceLabel NVARCHAR(200),
@GNDataSourceIcon NVARCHAR(300),
@GNDataSourceURLFormat NVARCHAR(300) = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[NameSourceGNFinder]
( 	[DataSourceID],
	[GNDataSourceName],
	[BHLIdentifierID],
	[GNDataSourceLabel],
	[GNDataSourceIcon],
	[GNDataSourceURLFormat] )
VALUES
( 	@DataSourceID,
	@GNDataSourceName,
	@BHLIdentifierID,
	@GNDataSourceLabel,
	@GNDataSourceIcon,
	@GNDataSourceURLFormat )

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.NameSourceGNFinderInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END
GO
