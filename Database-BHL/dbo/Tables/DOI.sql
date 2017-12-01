CREATE TABLE [dbo].[DOI] (
    [DOIID]            INT             IDENTITY (1, 1) NOT NULL,
    [DOIEntityTypeID]  INT             NOT NULL,
    [EntityID]         INT             NOT NULL,
    [DOIStatusID]      INT             NOT NULL,
    [DOIBatchID]       NVARCHAR (50)   CONSTRAINT [DF_DOI_DOIBatchID] DEFAULT ('') NOT NULL,
    [DOIName]          NVARCHAR (50)   CONSTRAINT [DF_DOIName] DEFAULT ('') NOT NULL,
    [StatusDate]       DATETIME        CONSTRAINT [DF_DOI_StatusDate] DEFAULT (getdate()) NOT NULL,
    [StatusMessage]    NVARCHAR (1000) CONSTRAINT [DF_DOI_StatusMessage] DEFAULT ('') NOT NULL,
    [IsValid]          SMALLINT        CONSTRAINT [DF_DOI_IsValid] DEFAULT ((0)) NOT NULL,
    [CreationDate]     DATETIME        CONSTRAINT [DF_DOI_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate] DATETIME        CONSTRAINT [DF_DOI_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_DOI] PRIMARY KEY CLUSTERED ([DOIID] ASC),
    CONSTRAINT [FK_DOI_DOIEntityType] FOREIGN KEY ([DOIEntityTypeID]) REFERENCES [dbo].[DOIEntityType] ([DOIEntityTypeID]),
    CONSTRAINT [FK_DOI_DOIStatus] FOREIGN KEY ([DOIStatusID]) REFERENCES [dbo].[DOIStatus] ([DOIStatusID])
);


GO
CREATE NONCLUSTERED INDEX [IX_DOI_TypeIDEntityID]
    ON [dbo].[DOI]([DOIEntityTypeID] ASC, [EntityID] ASC)
    INCLUDE([DOIName]);


GO
CREATE NONCLUSTERED INDEX [IX_DOI_EntityIsValid]
    ON [dbo].[DOI]([EntityID] ASC, [IsValid] ASC);


GO

CREATE NONCLUSTERED INDEX IX_DOI_TypeIDNameValidStatus 
	ON [dbo].[DOI]([DOIEntityTypeID],[DOIName],[IsValid],[DOIStatusID])
GO

CREATE NONCLUSTERED INDEX IX_DOI_NameIsValid 
	ON [dbo].[DOI]([DOIName],[IsValid])
GO
