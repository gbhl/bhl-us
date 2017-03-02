CREATE FULLTEXT INDEX ON [dbo].[SearchCatalogCreator]
    ([CreatorName] LANGUAGE 1033)
    KEY INDEX [PK_SearchCatalogCreator]
    ON ([BHLSearchCatalog], FILEGROUP [ftfg_BHLSearchCatalog])
    WITH STOPLIST [BHLFullTextStopList];


GO
CREATE FULLTEXT INDEX ON [dbo].[SearchCatalogKeyword]
    ([Keyword] LANGUAGE 1033)
    KEY INDEX [PK_SearchCatalogKeyword]
    ON ([BHLSearchCatalog], FILEGROUP [ftfg_BHLSearchCatalog])
    WITH STOPLIST [BHLFullTextStopList];


GO
CREATE FULLTEXT INDEX ON [dbo].[SearchCatalogSegment]
    ([SearchText] LANGUAGE 1033, [Title] LANGUAGE 1033, [TranslatedTitle] LANGUAGE 1033, [ContainerTitle] LANGUAGE 1033, [PublicationDetails] LANGUAGE 1033, [Volume] LANGUAGE 1033, [Series] LANGUAGE 1033, [Issue] LANGUAGE 1033, [Date] LANGUAGE 1033, [Subjects] LANGUAGE 1033, [SearchAuthors] LANGUAGE 1033)
    KEY INDEX [PK_SearchCatalogSegment]
    ON ([BHLSearchCatalog], FILEGROUP [ftfg_BHLSearchCatalog])
    WITH STOPLIST [BHLFullTextStopList];


GO
CREATE FULLTEXT INDEX ON [dbo].[SearchCatalog]
    ([SearchText] LANGUAGE 1033, [FullTitle] LANGUAGE 1033, [UniformTitle] LANGUAGE 1033, [PublicationDetails] LANGUAGE 1033, [PublisherPlace] LANGUAGE 1033, [PublisherName] LANGUAGE 1033, [Volume] LANGUAGE 1033, [EditionStatement] LANGUAGE 1033, [Subjects] LANGUAGE 1033, [Associations] LANGUAGE 1033, [Variants] LANGUAGE 1033, [SearchAuthors] LANGUAGE 1033)
    KEY INDEX [PK_SearchCatalog]
    ON ([BHLSearchCatalog], FILEGROUP [ftfg_BHLSearchCatalog])
    WITH STOPLIST [BHLFullTextStopList];

