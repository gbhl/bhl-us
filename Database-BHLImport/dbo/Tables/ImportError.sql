CREATE TABLE [dbo].[ImportError] (
    [ImportErrorID] INT             IDENTITY (1, 1) NOT NULL,
    [KeyType]       NVARCHAR (50)   NULL,
    [KeyValue]      NVARCHAR (40)   NULL,
    [ErrorDate]     DATETIME        CONSTRAINT [DF_ImportError_ErrorDate] DEFAULT (getdate()) NOT NULL,
    [Number]        INT             NULL,
    [Severity]      INT             NULL,
    [State]         INT             NULL,
    [Procedure]     NVARCHAR (126)  CONSTRAINT [DF_ImportError_Procedure] DEFAULT ('') NULL,
    [Line]          INT             NULL,
    [Message]       NVARCHAR (4000) CONSTRAINT [DF_ImportError_Message] DEFAULT ('') NULL,
    CONSTRAINT [PK_ImportError] PRIMARY KEY CLUSTERED ([ImportErrorID] ASC)
);

