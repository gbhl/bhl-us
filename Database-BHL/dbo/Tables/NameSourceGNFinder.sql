CREATE TABLE dbo.NameSourceGNFinder
(
	DataSourceID INT CONSTRAINT PK_NameSourceGNFinder PRIMARY KEY NOT NULL,
	GNDataSourceName nvarchar(200) CONSTRAINT DF_NameSourceGNFinder_DataSourceName DEFAULT ('') NOT NULL,
	BHLIdentifierID INT NOT NULL
)
GO
