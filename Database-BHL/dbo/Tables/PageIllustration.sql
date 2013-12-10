CREATE TABLE dbo. PageIllustration
        (
        PageIllustrationID int IDENTITY( 1,1 ) NOT NULL CONSTRAINT PK_PageIllustration PRIMARY KEY CLUSTERED,
        PageDetailID int NOT NULL,
        [Top] int NOT NULL DEFAULT (0),
        Bottom int NOT NULL DEFAULT (0),
        [Left] int NOT NULL DEFAULT (0),
        [Right] int NOT NULL DEFAULT (0),
        CreationDate datetime NOT NULL DEFAULT (GETDATE()),
        LastModifiedDate datetime NOT NULL DEFAULT (GETDATE()),
        CreationUserID int DEFAULT( 1),
        LastModifiedUserID int DEFAULT( 1),
        CONSTRAINT FK_PageIllustration_PageDetail FOREIGN KEY (PageDetailID ) REFERENCES dbo.PageDetail (PageDetailID)
        )
GO
