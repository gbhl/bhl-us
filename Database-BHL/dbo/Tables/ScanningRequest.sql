CREATE TABLE [dbo].[ScanningRequest] (
    [ScanningRequestID] INT            IDENTITY (1, 1) NOT NULL,
    [GeminiIssueID]     INT            NULL,
    [Title]             NVARCHAR (500) CONSTRAINT [DF_ScanningRequest_Title] DEFAULT ('') NOT NULL,
    [Year]              NVARCHAR (20)  CONSTRAINT [DF_ScanningRequest_Year] DEFAULT ('') NOT NULL,
    [Type]              NVARCHAR (20)  CONSTRAINT [DF_ScanningRequest_Type] DEFAULT ('') NOT NULL,
    [Volume]            NVARCHAR (100) CONSTRAINT [DF_ScanningRequest_Volume] DEFAULT ('') NOT NULL,
    [Edition]           NVARCHAR (100) CONSTRAINT [DF_ScanningRequest_Edition] DEFAULT ('') NOT NULL,
    [OCLC]              NVARCHAR (30)  CONSTRAINT [DF_ScanningRequest_OCLC] DEFAULT ('') NOT NULL,
    [ISBN]              NVARCHAR (30)  CONSTRAINT [DF_ScanningRequest_ISBN] DEFAULT ('') NOT NULL,
    [ISSN]              NVARCHAR (30)  CONSTRAINT [DF_ScanningRequest_ISSN] DEFAULT ('') NOT NULL,
    [Author]            NVARCHAR (200) CONSTRAINT [DF_ScanningRequest_Author] DEFAULT ('') NOT NULL,
    [Publisher]         NVARCHAR (200) CONSTRAINT [DF_ScanningRequest_Publisher] DEFAULT ('') NOT NULL,
    [Language]          NVARCHAR (20)  CONSTRAINT [DF_ScanningRequest_Language] DEFAULT ('') NOT NULL,
    [Note]              NVARCHAR (MAX) CONSTRAINT [DF_ScanningRequest_Note] DEFAULT ('') NOT NULL,
    [CreationDate]      DATETIME       CONSTRAINT [DF_ScanningRequest_CreationDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_ScanningRequest] PRIMARY KEY CLUSTERED ([ScanningRequestID] ASC)
);

