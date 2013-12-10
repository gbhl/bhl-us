CREATE TABLE dbo. PageDetail
        (
        PageDetailID int IDENTITY( 1,1 ) NOT NULL CONSTRAINT PK_PageDetail PRIMARY KEY CLUSTERED,
        PageID int NOT NULL,
        PageDetailStatusID int NOT NULL DEFAULT (10),
        StatusDate datetime NOT NULL DEFAULT (GETDATE()),
        Height int NOT NULL DEFAULT (0),
        Width int NOT NULL DEFAULT (0),
        PixelDepth int NOT NULL DEFAULT (0),
        AbbyyHasImage smallint NOT NULL DEFAULT (0),
        ContrastHasImage smallint NOT NULL DEFAULT (0),
        PercentCoverage decimal (5, 2) NOT NULL DEFAULT( 0),
        CreationDate datetime NOT NULL DEFAULT (GETDATE()),
        LastModifiedDate datetime NOT NULL DEFAULT (GETDATE()),
        CreationUserID int DEFAULT( 1),
        LastModifiedUserID int DEFAULT( 1),
        CONSTRAINT FK_PageDetail_PageDetailStatus FOREIGN KEY (PageDetailStatusID ) REFERENCES dbo.PageDetailStatus(PageDetailStatusID)
        )
GO
