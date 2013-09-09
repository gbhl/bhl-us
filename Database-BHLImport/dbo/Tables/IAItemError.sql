CREATE TABLE [dbo].[IAItemError] (
    [ItemErrorID] INT             IDENTITY (1, 1) NOT NULL,
    [ItemID]      INT             NULL,
    [ErrorDate]   DATETIME        CONSTRAINT [DF_ItemError_ErrorDate] DEFAULT (getdate()) NOT NULL,
    [Number]      INT             NULL,
    [Severity]    INT             NULL,
    [State]       INT             NULL,
    [Procedure]   NVARCHAR (126)  CONSTRAINT [DF_ItemError_Procedure] DEFAULT ('') NULL,
    [Line]        INT             NULL,
    [Message]     NVARCHAR (4000) CONSTRAINT [DF_ItemError_Message] DEFAULT ('') NULL,
    CONSTRAINT [PK_ItemError] PRIMARY KEY CLUSTERED ([ItemErrorID] ASC),
    CONSTRAINT [FK_ItemError_Item] FOREIGN KEY ([ItemID]) REFERENCES [dbo].[IAItem] ([ItemID])
);

