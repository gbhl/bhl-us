CREATE TABLE [dbo].[ItemInstitution](
	[ItemInstitutionID] [int] IDENTITY(1,1) NOT NULL,
	[ItemID] [int] NOT NULL,
	[InstitutionCode] [nvarchar](10) NOT NULL,
	[InstitutionRoleID] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL CONSTRAINT [DF_ItemInstitution_CreationDate]  DEFAULT (getdate()),
	[LastModifiedDate] [datetime] NOT NULL CONSTRAINT [DF_ItemInstitution_LastModifiedDate]  DEFAULT (getdate()),
	[CreationUserID] [int] NOT NULL CONSTRAINT [DF_ItemInstitution_CreationUserID]  DEFAULT ((1)),
	[LastModifiedUserID] [int] NOT NULL CONSTRAINT [DF_ItemInstitution_LastModifiedUserID]  DEFAULT ((1)),
	CONSTRAINT [PK_ItemInstitution] PRIMARY KEY CLUSTERED ([ItemInstitutionID] ASC)
)
GO

ALTER TABLE [dbo].[ItemInstitution]  WITH CHECK ADD CONSTRAINT [FK_ItemInstitution_Institution] FOREIGN KEY([InstitutionCode]) REFERENCES [dbo].[Institution] ([InstitutionCode])
GO

ALTER TABLE [dbo].[ItemInstitution]  WITH CHECK ADD CONSTRAINT [FK_ItemInstitution_InstitutionRole] FOREIGN KEY([InstitutionRoleID]) REFERENCES [dbo].[InstitutionRole] ([InstitutionRoleID])
GO

ALTER TABLE [dbo].[ItemInstitution]  WITH CHECK ADD CONSTRAINT [FK_ItemInstitution_Item] FOREIGN KEY([ItemID]) REFERENCES [dbo].[Item] ([ItemID])
GO

CREATE NONCLUSTERED INDEX [IX_ItemInstitution_ItemID]
	ON [dbo].[ItemInstitution] ([ItemID] ASC)
	INCLUDE ([InstitutionCode],[InstitutionRoleID])
GO
