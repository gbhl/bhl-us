CREATE TABLE [dbo].[IAMarcControl] (
    [MarcControlID]    INT             IDENTITY (1, 1) NOT NULL,
    [MarcID]           INT             NOT NULL,
    [Tag]              NCHAR (3)       CONSTRAINT [DF_MarcControl_Tag] DEFAULT ('') NOT NULL,
    [Value]            NVARCHAR (2000) CONSTRAINT [DF_MarcControl_Value] DEFAULT ('') NOT NULL,
    [CreatedDate]      DATETIME        CONSTRAINT [DF_MarcControl_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate] DATETIME        CONSTRAINT [DF_MarcControl_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_MarcControl] PRIMARY KEY CLUSTERED ([MarcControlID] ASC),
    CONSTRAINT [FK_MarcControl_Marc] FOREIGN KEY ([MarcID]) REFERENCES [dbo].[IAMarc] ([MarcID])
);


GO
CREATE NONCLUSTERED INDEX [IX_IAMarcControl_MarcIDTagValue]
    ON [dbo].[IAMarcControl]([MarcID] ASC, [Tag] ASC, [Value] ASC);

