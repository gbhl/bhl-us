CREATE TABLE [audit].[AuditBasicSummary] (
    [AuditBasicSummaryID] INT            IDENTITY (1, 1) NOT NULL,
    [AuditDate]           DATETIME       NOT NULL,
    [EntityName]          NVARCHAR (50)  NOT NULL,
    [Operation]           NCHAR (1)      NOT NULL,
    [EntityKey1]          NVARCHAR (100) NOT NULL,
    [EntityKey2]          NVARCHAR (100) NULL,
    [EntityKey3]          NVARCHAR (100) NULL,
    [ApplicationUserID]   INT            NULL,
    CONSTRAINT [PK_AuditBasicSummary] PRIMARY KEY CLUSTERED ([AuditBasicSummaryID] ASC)
);

