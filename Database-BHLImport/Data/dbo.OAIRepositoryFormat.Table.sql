USE [BHLImport]
GO
SET IDENTITY_INSERT [dbo].[OAIRepositoryFormat] ON 

INSERT [dbo].[OAIRepositoryFormat] ([RepositoryFormatID], [RepositoryID], [FormatID], [Schema], [Namespace], [CreationDate], [LastModifiedDate]) VALUES (5, 2, 1, N'http://www.openarchives.org/OAI/2.0/oai_dc.xsd', N'http://www.openarchives.org/OAI/2.0/oai_dc/', CAST(0x0000A25300BA4BCD AS DateTime), CAST(0x0000A25300BA4BCD AS DateTime))
INSERT [dbo].[OAIRepositoryFormat] ([RepositoryFormatID], [RepositoryID], [FormatID], [Schema], [Namespace], [CreationDate], [LastModifiedDate]) VALUES (6, 2, 2, N'http://www.loc.gov/standards/mods/v3/mods-3-1.xsd', N'http://www.loc.gov/mods/v3', CAST(0x0000A25300BA518C AS DateTime), CAST(0x0000A25300BA518C AS DateTime))
INSERT [dbo].[OAIRepositoryFormat] ([RepositoryFormatID], [RepositoryID], [FormatID], [Schema], [Namespace], [CreationDate], [LastModifiedDate]) VALUES (7, 3, 1, N'http://www.openarchives.org/OAI/2.0/oai_dc.xsd', N'http://www.openarchives.org/OAI/2.0/oai_dc/', CAST(0x0000A25300BA851D AS DateTime), CAST(0x0000A25300BA851D AS DateTime))
INSERT [dbo].[OAIRepositoryFormat] ([RepositoryFormatID], [RepositoryID], [FormatID], [Schema], [Namespace], [CreationDate], [LastModifiedDate]) VALUES (8, 3, 4, N'http://www.purl.org/agmes/agrisap/schema/agris.xsd', N'http://www.purl.org/agmes/agrisap/schema/', CAST(0x0000A25300BA8CA3 AS DateTime), CAST(0x0000A25300BA8CA3 AS DateTime))
INSERT [dbo].[OAIRepositoryFormat] ([RepositoryFormatID], [RepositoryID], [FormatID], [Schema], [Namespace], [CreationDate], [LastModifiedDate]) VALUES (14, 6, 1, N'http://www.openarchives.org/OAI/2.0/oai_dc.xsd', N'http://www.openarchives.org/OAI/2.0/oai_dc/', CAST(0x0000A25300BDBB94 AS DateTime), CAST(0x0000A25300BDBB94 AS DateTime))
SET IDENTITY_INSERT [dbo].[OAIRepositoryFormat] OFF
