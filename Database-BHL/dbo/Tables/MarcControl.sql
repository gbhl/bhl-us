CREATE TABLE [dbo].[MarcControl] (
    [MarcControlID]    INT            IDENTITY (1, 1) NOT NULL,
    [MarcID]           INT            NOT NULL,
    [Tag]              NCHAR (3)      CONSTRAINT [DF_MarcControl_Tag] DEFAULT ('') NOT NULL,
    [Value]            NVARCHAR (200) CONSTRAINT [DF_MarcControl_Value] DEFAULT ('') NOT NULL,
    [CreationDate]     DATETIME       CONSTRAINT [DF_MarcControl_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate] DATETIME       CONSTRAINT [DF_MarcControl_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_MarcControl] PRIMARY KEY CLUSTERED ([MarcControlID] ASC),
    CONSTRAINT [FK_MarcControl_Marc] FOREIGN KEY ([MarcID]) REFERENCES [dbo].[Marc] ([MarcID]) ON DELETE CASCADE
);

