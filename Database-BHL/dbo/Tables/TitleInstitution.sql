CREATE TABLE [dbo].[TitleInstitution](
	[TitleInstitutionID] [int] IDENTITY(1,1) NOT NULL,
	[TitleID] [int] NOT NULL,
	[InstitutionCode] [nvarchar](10) NOT NULL,
	[InstitutionRoleID] [int] NOT NULL,
	[Url] [nvarchar](500) NOT NULL CONSTRAINT [DF_TitleInstitution_Url] DEFAULT(''),
	[CreationDate] [datetime] NOT NULL CONSTRAINT [DF_TitleInstitution_CreationDate]  DEFAULT (getdate()),
	[LastModifiedDate] [datetime] NOT NULL CONSTRAINT [DF_TitleInstitution_LastModifiedDate]  DEFAULT (getdate()),
	[CreationUserID] [int] NOT NULL CONSTRAINT [DF_TitleInstitution_CreationUserID]  DEFAULT ((1)),
	[LastModifiedUserID] [int] NOT NULL CONSTRAINT [DF_TitleInstitution_LastModifiedUserID]  DEFAULT ((1)),
	CONSTRAINT [PK_TitleInstitution] PRIMARY KEY CLUSTERED ( [TitleInstitutionID] ASC )
)
GO

ALTER TABLE [dbo].[TitleInstitution] WITH CHECK ADD CONSTRAINT [FK_TitleInstitution_Intitution] FOREIGN KEY([InstitutionCode]) REFERENCES [dbo].[Institution] ([InstitutionCode])
GO

ALTER TABLE [dbo].[TitleInstitution] WITH CHECK ADD CONSTRAINT [FK_TitleInstitution_InstitutionRole] FOREIGN KEY([InstitutionRoleID]) REFERENCES [dbo].[InstitutionRole] ([InstitutionRoleID])
GO

ALTER TABLE [dbo].[TitleInstitution] WITH CHECK ADD CONSTRAINT [FK_TitleInstitution_Title] FOREIGN KEY([TitleID]) REFERENCES [dbo].[Title] ([TitleID])
GO

CREATE NONCLUSTERED INDEX [IX_TitleInstitution_TitleID]
	ON [dbo].[TitleInstitution] ([TitleID] ASC)
	INCLUDE ([InstitutionCode],[InstitutionRoleID])
GO

CREATE NONCLUSTERED INDEX [IX_TitleInstitution_InstitutionCode] 
	ON [dbo].[TitleInstitution] ([InstitutionCode] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_TitleInstitution_InstitutionRole] 
	ON [dbo].[TitleInstitution] ([InstitutionRoleID] ASC)
	INCLUDE ([TitleID], [InstitutionCode])
GO
