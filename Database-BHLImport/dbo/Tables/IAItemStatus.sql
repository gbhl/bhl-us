CREATE TABLE [dbo].[IAItemStatus] (
    [ItemStatusID]     INT             NOT NULL,
    [Status]           NVARCHAR (30)   CONSTRAINT [DF__ItemStatu__Statu__04459E07] DEFAULT ('') NOT NULL,
    [Description]      NVARCHAR (4000) DEFAULT ('') NOT NULL,
    [CreatedDate]      DATETIME        DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate] DATETIME        DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([ItemStatusID] ASC)
);

