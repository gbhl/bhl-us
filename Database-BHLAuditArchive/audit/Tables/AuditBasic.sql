CREATE TABLE [audit].[AuditBasic] (
    [AuditBasicID]      INT            NOT NULL,
    [AuditDate]         DATETIME       NOT NULL,
    [EntityName]        NVARCHAR (50)  NOT NULL,
    [Operation]         NCHAR (1)      NOT NULL,
    [EntityKey1]        NVARCHAR (100) NOT NULL,
    [EntityKey2]        NVARCHAR (100) NULL,
    [EntityKey3]        NVARCHAR (100) NULL,
    [ApplicationUserID] INT            NULL,
    [SystemUserID]      NVARCHAR (128) NOT NULL,
    [SQLStatement]      NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_AuditBasic] PRIMARY KEY CLUSTERED ([AuditBasicID] ASC)
);

