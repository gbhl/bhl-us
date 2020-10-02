CREATE TABLE dbo.NameSourceGNFinder
(
	DataSourceID INT CONSTRAINT PK_NameSourceGNFinder PRIMARY KEY NOT NULL,
	GNDataSourceName nvarchar(200) CONSTRAINT DF_NameSourceGNFinder_DataSourceName DEFAULT ('') NOT NULL,
	BHLIdentifierID INT NOT NULL,
	GNDataSourceLabel nvarchar(200) CONSTRAINT DF_NameSourceGNFinder_GNDataSourceLabel DEFAULT ('') NOT NULL,
	GNDataSourceIcon nvarchar(300) CONSTRAINT DF_NameSourceGNFinder_GNDataSourceIcon DEFAULT ('') NOT NULL,
	GNDataSourceURLFormat nvarchar(300) CONSTRAINT DF_NameSourceGNFinder_GNDataSourceURLFormat DEFAULT ('') NOT NULL
)
GO
