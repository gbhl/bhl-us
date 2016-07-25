CREATE TABLE [dbo].[Institution] (
    [InstitutionCode]    NVARCHAR (10)  NOT NULL,
    [InstitutionName]    NVARCHAR (255) NOT NULL,
    [Note]               NVARCHAR (255) NULL,
    [InstitutionUrl]     NVARCHAR (255) NULL,
    [BHLMemberLibrary]   BIT            CONSTRAINT [DF_Institution_BHLMemberLibrary] DEFAULT ((0)) NOT NULL,
	[CreationDate]       DATETIME       CONSTRAINT [DF_Institution_CreationDate] DEFAULT getdate() NULL,
	[LastModifiedDate]   DATETIME       CONSTRAINT [DF_Institution_LastModifiedDate] DEFAULT getdate() NULL,
	[CreationUserID]     INT            CONSTRAINT [DF_Institution_CreationUserID] DEFAULT 1 NULL,
	[LastModifiedUserID] INT            CONSTRAINT [DF_Institution_LastModifiedUserID] DEFAULT 1 NULL,
    CONSTRAINT [aaaaaInstitution_PK] PRIMARY KEY CLUSTERED ([InstitutionCode] ASC)
);
GO
