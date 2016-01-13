CREATE TABLE [dbo].[ImportStatus] (
    [ImportStatusID]   INT             NOT NULL,
    [Status]           NVARCHAR (20)   NOT NULL,
    [Description]      NVARCHAR (4000) CONSTRAINT [DF_ImportStatus_Description] DEFAULT ('') NOT NULL,
    [CreatedDate]      DATETIME        CONSTRAINT [DF_ImportStatus_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate] DATETIME        CONSTRAINT [DF_ImportStatus_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [aaaaaImportStatus_PK] PRIMARY KEY CLUSTERED ([ImportStatusID] ASC)
);

