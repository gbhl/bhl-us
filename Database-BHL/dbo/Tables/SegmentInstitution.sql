CREATE TABLE [dbo].[SegmentInstitution](
	[SegmentInstitutionID] [int] IDENTITY(1,1) NOT NULL,
	[SegmentID] [int] NOT NULL,
	[InstitutionCode] [nvarchar](10) NOT NULL,
	[InstitutionRoleID] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL CONSTRAINT [DF_SegmentInstitution_CreationDate]  DEFAULT (getdate()),
	[LastModifiedDate] [datetime] NOT NULL CONSTRAINT [DF_SegmentInstitution_LastModifiedDate]  DEFAULT (getdate()),
	[CreationUserID] [int] NOT NULL CONSTRAINT [DF_SegmentInstitution_CreationUserID]  DEFAULT ((1)),
	[LastModifiedUserID] [int] NOT NULL CONSTRAINT [DF_SegmentInstitution_LastModifiedUserID]  DEFAULT ((1)),
	CONSTRAINT [PK_SegmentInstitution] PRIMARY KEY CLUSTERED ([SegmentInstitutionID] ASC)
)
GO

ALTER TABLE [dbo].[SegmentInstitution]  WITH CHECK ADD CONSTRAINT [FK_SegmentInstitution_Institution] FOREIGN KEY([InstitutionCode]) REFERENCES [dbo].[Institution] ([InstitutionCode])
GO

ALTER TABLE [dbo].[SegmentInstitution]  WITH CHECK ADD CONSTRAINT [FK_SegmentInstitution_InstitutionRole] FOREIGN KEY([InstitutionRoleID]) REFERENCES [dbo].[InstitutionRole] ([InstitutionRoleID])
GO

ALTER TABLE [dbo].[SegmentInstitution]  WITH CHECK ADD CONSTRAINT [FK_SegmentInstitution_Segment] FOREIGN KEY([SegmentID]) REFERENCES [dbo].[Segment] ([SegmentID])
GO

CREATE NONCLUSTERED INDEX [IX_SegmentInstitution_SegmentID]
	ON [dbo].[SegmentInstitution] ([SegmentID] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_SegmentInsitution_InstitutionCode] 
	ON [dbo].[SegmentInstitution] ([InstitutionCode] ASC)
	INCLUDE ([SegmentID], [InstitutionRoleID])
GO
