CREATE TABLE [dbo].[Vault] (
    [VaultID]             INT            NOT NULL,
    [Server]              NVARCHAR (30)  NULL,
    [FolderShare]         NVARCHAR (30)  NULL,
    [WebVirtualDirectory] NVARCHAR (30)  NULL,
    [OCRFolderShare]      NVARCHAR (100) NULL,
    [IsCurrent]           TINYINT        NOT NULL,
    CONSTRAINT [aaaaaVault_PK] PRIMARY KEY CLUSTERED ([VaultID] ASC)
);
GO
CREATE UNIQUE NONCLUSTERED INDEX [CatalogID]
    ON [dbo].[Vault]([VaultID] ASC);
GO
ALTER TABLE [dbo].[Vault] ADD CONSTRAINT [DF_Vault_IsCurrent] DEFAULT ((0)) FOR [IsCurrent]
GO

