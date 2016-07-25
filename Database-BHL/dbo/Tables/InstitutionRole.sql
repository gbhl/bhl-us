CREATE TABLE [dbo].[InstitutionRole](
	[InstitutionRoleID] [int] IDENTITY(1,1) NOT NULL,
	[InstitutionRoleName] [nvarchar](100) NOT NULL CONSTRAINT [DF_InstitutionRole_InstitutionRoleName]  DEFAULT (''),
	[InstitutionRoleLabel] [nvarchar](100) NOT NULL CONSTRAINT [DF_InstitutionRole_InstitutionRoleLabel]  DEFAULT (''),
	[CreationDate] [datetime] NOT NULL CONSTRAINT [DF_InstitutionRole_CreationDate]  DEFAULT (getdate()),
	[LastModifiedDate] [datetime] NOT NULL CONSTRAINT [DF_InstitutionRole_LastModifiedDate]  DEFAULT (getdate()),
	[CreationUserID] [int] NOT NULL CONSTRAINT [DF_InstitutionRole_CreationUserID]  DEFAULT ((1)),
	[LastModifiedUserID] [int] NOT NULL CONSTRAINT [DF_InstitutionRole_LastModifiedUserID]  DEFAULT ((1)),
	CONSTRAINT [PK_InstitutionRole] PRIMARY KEY CLUSTERED ([InstitutionRoleID] ASC)
)
GO

