CREATE TABLE [annotation].[AnnotatedPageCharacteristic] (
    [AnnotatedPageCharacteristicID] INT            IDENTITY (1, 1) NOT NULL,
    [AnnotatedPageID]               INT            NULL,
    [CharacteristicDetail]          NVARCHAR (MAX) CONSTRAINT [DF_AnnotatedPageCharacteristic_CharacteristicDetail] DEFAULT ('') NOT NULL,
    [CharacteristicDetailClean]     NVARCHAR (MAX) CONSTRAINT [DF_AnnotatedPageCharacteristic_CharacteristicDetailClean] DEFAULT ('') NOT NULL,
    [CharacteristicDetailDisplay]   NVARCHAR (MAX) CONSTRAINT [DF_AnnotatedPageCharacteristic_CharacteristicsDetailDisplay] DEFAULT ('') NOT NULL,
    [CreationDate]                  DATETIME       CONSTRAINT [DF_AnnotatedPageCharacteristic_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]              DATETIME       CONSTRAINT [DF_AnnotatedPageCharacteristic_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_AnnotatedPageCharacteristic] PRIMARY KEY CLUSTERED ([AnnotatedPageCharacteristicID] ASC),
    CONSTRAINT [FK_AnnotatedPageCharacteristic_AnnotatedPage] FOREIGN KEY ([AnnotatedPageID]) REFERENCES [annotation].[AnnotatedPage] ([AnnotatedPageID])
);

