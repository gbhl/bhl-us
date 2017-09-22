CREATE TABLE [dbo].[Vault] (
    [VaultID]             INT            NOT NULL,
    [Server]              NVARCHAR (30)  NULL,
    [FolderShare]         NVARCHAR (30)  NULL,
    [WebVirtualDirectory] NVARCHAR (30)  NULL,
    [OCRFolderShare]      NVARCHAR (100) NULL,
    CONSTRAINT [aaaaaVault_PK] PRIMARY KEY CLUSTERED ([VaultID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [CatalogID]
    ON [dbo].[Vault]([VaultID] ASC);


GO
