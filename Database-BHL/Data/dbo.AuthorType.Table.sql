SET IDENTITY_INSERT [dbo].[AuthorType] ON 

INSERT [dbo].[AuthorType] ([AuthorTypeID], [AuthorTypeName], [CreationDate], [LastModifiedDate], [CreationUserID], [LastModifiedUserID]) VALUES (1, N'Person', CAST(N'2012-06-14 11:01:11.000' AS DateTime), CAST(N'2012-06-14 11:01:11.000' AS DateTime), 1, 1)
INSERT [dbo].[AuthorType] ([AuthorTypeID], [AuthorTypeName], [CreationDate], [LastModifiedDate], [CreationUserID], [LastModifiedUserID]) VALUES (2, N'Corporation', CAST(N'2012-06-14 11:01:11.000' AS DateTime), CAST(N'2012-06-14 11:01:11.000' AS DateTime), 1, 1)
INSERT [dbo].[AuthorType] ([AuthorTypeID], [AuthorTypeName], [CreationDate], [LastModifiedDate], [CreationUserID], [LastModifiedUserID]) VALUES (3, N'Meeting', CAST(N'2012-06-14 11:01:11.000' AS DateTime), CAST(N'2012-06-14 11:01:11.000' AS DateTime), 1, 1)
SET IDENTITY_INSERT [dbo].[AuthorType] OFF
