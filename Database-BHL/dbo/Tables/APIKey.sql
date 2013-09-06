CREATE TABLE [dbo].[APIKey] (
    [ApiKeyID]         INT              IDENTITY (1, 1) NOT NULL,
    [ContactName]      NVARCHAR (200)   CONSTRAINT [DF__APIKey__ContactN__53CD4F35] DEFAULT ('') NOT NULL,
    [EmailAddress]     NVARCHAR (200)   CONSTRAINT [DF__APIKey__EmailAdd__54C1736E] DEFAULT ('') NOT NULL,
    [ApiKeyValue]      UNIQUEIDENTIFIER CONSTRAINT [DF__APIKey__ApiKeyVa__55B597A7] DEFAULT (newid()) NOT NULL,
    [IsActive]         TINYINT          CONSTRAINT [DF_APIKey_IsActive] DEFAULT ((1)) NOT NULL,
    [CreationDate]     DATETIME         CONSTRAINT [DF_APIKey_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate] DATETIME         CONSTRAINT [DF_APIKey_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_APIKey] PRIMARY KEY CLUSTERED ([ApiKeyID] ASC)
);

