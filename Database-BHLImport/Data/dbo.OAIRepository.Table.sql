USE [BHLImport]
GO
SET IDENTITY_INSERT [dbo].[OAIRepository] ON 

INSERT [dbo].[OAIRepository] ([RepositoryID], [ImportSourceID], [BHLInstitutionCode], [RepositoryName], [BaseUrl], [ProtocolVersion], [EarliestDateStamp], [DeletedRecord], [Granularity], [CreationDate], [LastModifiedDate]) VALUES (2, 4, N'PENSOFT', N'Pensoft Publishers', N'http://oai.pensoft.eu', N'2.0', CAST(0x00009ACF00000000 AS DateTime), N'no', N'yyyy-MM-dd', CAST(0x0000A25300AB6432 AS DateTime), CAST(0x0000A25300AB6432 AS DateTime))
INSERT [dbo].[OAIRepository] ([RepositoryID], [ImportSourceID], [BHLInstitutionCode], [RepositoryName], [BaseUrl], [ProtocolVersion], [EarliestDateStamp], [DeletedRecord], [Granularity], [CreationDate], [LastModifiedDate]) VALUES (3, 5, N'SCIELO', N'SciELO - Scientific Electronic Library Online', N'http://www.scielo.br/oai/scielo-oai.php', N'2.0', CAST(0x00000D3100000000 AS DateTime), N'no', N'yyyy-MM-dd', CAST(0x0000A25300ABA2AD AS DateTime), CAST(0x0000A25300ABA2AD AS DateTime))
INSERT [dbo].[OAIRepository] ([RepositoryID], [ImportSourceID], [BHLInstitutionCode], [RepositoryName], [BaseUrl], [ProtocolVersion], [EarliestDateStamp], [DeletedRecord], [Granularity], [CreationDate], [LastModifiedDate]) VALUES (6, 7, N'BDRJBM', N'Bibdigital Real Jardin Botanico', N'http://bibdigital.rjb.csic.es/oai', N'2.0', CAST(0x0000A24400E8A760 AS DateTime), N'no', N'yyyy-MM-dd HH:mm:ss', CAST(0x0000A25300BD84A1 AS DateTime), CAST(0x0000A25300BD84A1 AS DateTime))
SET IDENTITY_INSERT [dbo].[OAIRepository] OFF
