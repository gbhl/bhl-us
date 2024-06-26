USE [BHLImport]
GO
SET IDENTITY_INSERT [dbo].[OAIFormat] ON 

INSERT [dbo].[OAIFormat] ([FormatID], [Prefix], [AssemblyName], [CreationDate], [LastModifiedDate]) VALUES (1, N'oai_dc', N'MOBOT.BHL.OAIDC', CAST(0x0000A25300D4E5F7 AS DateTime), CAST(0x0000A25300D4E5F7 AS DateTime))
INSERT [dbo].[OAIFormat] ([FormatID], [Prefix], [AssemblyName], [CreationDate], [LastModifiedDate]) VALUES (2, N'mods', N'MOBOT.BHL.OAIMODS', CAST(0x0000A25300D4E756 AS DateTime), CAST(0x0000A25300D4E756 AS DateTime))
INSERT [dbo].[OAIFormat] ([FormatID], [Prefix], [AssemblyName], [CreationDate], [LastModifiedDate]) VALUES (3, N'mets', N'', CAST(0x0000A25300D4E968 AS DateTime), CAST(0x0000A25300D4E968 AS DateTime))
INSERT [dbo].[OAIFormat] ([FormatID], [Prefix], [AssemblyName], [CreationDate], [LastModifiedDate]) VALUES (4, N'oai_dc_agris', N'', CAST(0x0000A25300D4F0E1 AS DateTime), CAST(0x0000A25300D4F0E1 AS DateTime))
INSERT [dbo].[OAIFormat] ([FormatID], [Prefix], [AssemblyName], [CreationDate], [LastModifiedDate]) VALUES (5, N'ore', N'', CAST(0x0000A25300D4F647 AS DateTime), CAST(0x0000A25300D4F647 AS DateTime))
INSERT [dbo].[OAIFormat] ([FormatID], [Prefix], [AssemblyName], [CreationDate], [LastModifiedDate]) VALUES (6, N'qdc', N'', CAST(0x0000A25300D4F7A2 AS DateTime), CAST(0x0000A25300D4F7A2 AS DateTime))
INSERT [dbo].[OAIFormat] ([FormatID], [Prefix], [AssemblyName], [CreationDate], [LastModifiedDate]) VALUES (7, N'rdf', N'', CAST(0x0000A25300D4FBE3 AS DateTime), CAST(0x0000A25300D4FBE3 AS DateTime))
SET IDENTITY_INSERT [dbo].[OAIFormat] OFF
