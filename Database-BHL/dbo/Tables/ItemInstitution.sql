SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemInstitution](
	[ItemInstitutionID] [int] IDENTITY(1,1) NOT NULL,
	[ItemID] [int] NOT NULL,
	[InstitutionCode] [nvarchar](10) NOT NULL,
	[InstitutionRoleID] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[CreationUserID] [int] NOT NULL,
	[LastModifiedUserID] [int] NOT NULL,
 CONSTRAINT [PK_ItemInstitution] PRIMARY KEY CLUSTERED 
(
	[ItemInstitutionID] ASC
)
)

GO
SET ANSI_PADDING ON

GO
CREATE NONCLUSTERED INDEX [IX_ItemInstitution_InstitutionCode] ON [dbo].[ItemInstitution]
(
	[InstitutionCode] ASC
)
GO
SET ANSI_PADDING ON

GO
CREATE NONCLUSTERED INDEX [IX_ItemInstitution_InstitutionRole] ON [dbo].[ItemInstitution]
(
	[InstitutionRoleID] ASC
)
INCLUDE ( 	[ItemID],
	[InstitutionCode])
GO
SET ANSI_PADDING ON

GO
CREATE NONCLUSTERED INDEX [IX_ItemInstitution_ItemID] ON [dbo].[ItemInstitution]
(
	[ItemID] ASC
)
INCLUDE ( 	[InstitutionCode],
	[InstitutionRoleID])
GO
ALTER TABLE [dbo].[ItemInstitution] ADD  CONSTRAINT [DF_ItemInstitution_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[ItemInstitution] ADD  CONSTRAINT [DF_ItemInstitution_LastModifiedDate]  DEFAULT (getdate()) FOR [LastModifiedDate]
GO
ALTER TABLE [dbo].[ItemInstitution] ADD  CONSTRAINT [DF_ItemInstitution_CreationUserID]  DEFAULT ((1)) FOR [CreationUserID]
GO
ALTER TABLE [dbo].[ItemInstitution] ADD  CONSTRAINT [DF_ItemInstitution_LastModifiedUserID]  DEFAULT ((1)) FOR [LastModifiedUserID]
GO
ALTER TABLE [dbo].[ItemInstitution]  WITH CHECK ADD  CONSTRAINT [FK_ItemInstitution_Institution] FOREIGN KEY([InstitutionCode])
REFERENCES [dbo].[Institution] ([InstitutionCode])
GO
ALTER TABLE [dbo].[ItemInstitution] CHECK CONSTRAINT [FK_ItemInstitution_Institution]
GO
ALTER TABLE [dbo].[ItemInstitution]  WITH CHECK ADD  CONSTRAINT [FK_ItemInstitution_InstitutionRole] FOREIGN KEY([InstitutionRoleID])
REFERENCES [dbo].[InstitutionRole] ([InstitutionRoleID])
GO
ALTER TABLE [dbo].[ItemInstitution] CHECK CONSTRAINT [FK_ItemInstitution_InstitutionRole]
GO
ALTER TABLE [dbo].[ItemInstitution]  WITH CHECK ADD  CONSTRAINT [FK_ItemInstitution_Item] FOREIGN KEY([ItemID])
REFERENCES [dbo].[Item] ([ItemID])
GO
ALTER TABLE [dbo].[ItemInstitution] CHECK CONSTRAINT [FK_ItemInstitution_Item]
GO
