SET IDENTITY_INSERT [dbo].[InstitutionGroup] ON 

INSERT [dbo].[InstitutionGroup] ([InstitutionGroupID], [InstitutionGroupName], [InstitutionGroupDescription], [CreationDate], [LastModifiedDate], [CreationUserID], [LastModifiedUserID]) VALUES (1, N'Member', N'Institutions and individuals in this group are BHL Members', CAST(N'2020-11-23 09:50:57.277' AS DateTime), CAST(N'2020-11-23 09:50:57.277' AS DateTime), 2, 2)
INSERT [dbo].[InstitutionGroup] ([InstitutionGroupID], [InstitutionGroupName], [InstitutionGroupDescription], [CreationDate], [LastModifiedDate], [CreationUserID], [LastModifiedUserID]) VALUES (2, N'Affiliate', N'Institutions and persons in this group are BHL Affiliates', CAST(N'2020-11-23 09:51:09.010' AS DateTime), CAST(N'2020-11-23 09:51:09.010' AS DateTime), 2, 2)
SET IDENTITY_INSERT [dbo].[InstitutionGroup] OFF
