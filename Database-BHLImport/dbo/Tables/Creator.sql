CREATE TABLE [dbo].[Creator] (
    [CreatorID]                INT            IDENTITY (1, 1) NOT NULL,
    [ImportStatusID]           INT            NOT NULL,
    [ImportSourceID]           INT            NULL,
    [CreatorName]              NVARCHAR (255) NOT NULL,
    [FirstNameFirst]           NVARCHAR (255) NULL,
    [SimpleName]               NVARCHAR (255) NULL,
    [DOB]                      NVARCHAR (50)  NULL,
    [DOD]                      NVARCHAR (50)  NULL,
    [Biography]                NTEXT          NULL,
    [CreatorNote]              NVARCHAR (255) NULL,
    [MARCDataFieldTag]         NVARCHAR (3)   NULL,
    [MARCCreator_a]            NVARCHAR (450) NULL,
    [MARCCreator_b]            NVARCHAR (450) NULL,
    [MARCCreator_c]            NVARCHAR (450) NULL,
    [MARCCreator_d]            NVARCHAR (450) NULL,
    [MARCCreator_Full]         NVARCHAR (450) NULL,
    [ExternalCreationDate]     DATETIME       NULL,
    [ExternalLastModifiedDate] DATETIME       NULL,
    [ProductionDate]           DATETIME       NULL,
    [CreatedDate]              DATETIME       CONSTRAINT [DF_Creator_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]         DATETIME       CONSTRAINT [DF_Creator_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    [MARCCreator_q]            NVARCHAR (450) NULL,
    CONSTRAINT [PK_Creator] PRIMARY KEY CLUSTERED ([CreatorID] ASC),
    CONSTRAINT [FK_Creator_ImportSource] FOREIGN KEY ([ImportSourceID]) REFERENCES [dbo].[ImportSource] ([ImportSourceID]),
    CONSTRAINT [FK_Creator_ImportStatus] FOREIGN KEY ([ImportStatusID]) REFERENCES [dbo].[ImportStatus] ([ImportStatusID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Creator]
    ON [dbo].[Creator]([CreatorID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Creator_MarcCreator]
    ON [dbo].[Creator]([MARCCreator_a] ASC)
    INCLUDE([MARCCreator_b], [MARCCreator_c], [MARCCreator_d], [MARCCreator_q]);

