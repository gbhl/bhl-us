CREATE TABLE [audit].[AuditBasic] (
    [AuditBasicID]      INT            IDENTITY (1, 1) NOT NULL,
    [AuditDate]         DATETIME       CONSTRAINT [DF__AuditBasi__Audit__0075D87A] DEFAULT (getdate()) NOT NULL,
    [EntityName]        NVARCHAR (50)  CONSTRAINT [DF__AuditBasi__Entit__0169FCB3] DEFAULT ('') NOT NULL,
    [Operation]         NCHAR (1)      NOT NULL,
    [EntityKey1]        NVARCHAR (100) NOT NULL,
    [EntityKey2]        NVARCHAR (100) NULL,
    [EntityKey3]        NVARCHAR (100) NULL,
    [ApplicationUserID] INT            NULL,
    [SystemUserID]      NVARCHAR (128) NOT NULL,
    [SQLStatement]      NVARCHAR (MAX) CONSTRAINT [DF__AuditBasi__SQLSt__025E20EC] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_AuditBasic] PRIMARY KEY CLUSTERED ([AuditBasicID] ASC)
);

