CREATE TABLE dbo.ApplicationCache
	(
	CacheKey nvarchar(100) CONSTRAINT PK_ApplicationCache PRIMARY KEY NOT NULL,
	CacheData nvarchar(max) CONSTRAINT DF_ApplicationCache_CacheData DEFAULT('') NOT NULL,
	AbsoluteExpirationDate datetime NULL,
	SlidingExpirationDuration int NULL,
	LastAccessDate datetime CONSTRAINT DF_ApplicationCache_LastAccessDate DEFAULT(GETDATE()) NOT NULL,
	CreationDate datetime CONSTRAINT DF_ApplicationCache_CreationDate DEFAULT(GETDATE()) NOT NULL
)
GO
