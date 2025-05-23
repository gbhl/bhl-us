USE [BHLImport]
GO
SET IDENTITY_INSERT [dbo].[OAIHarvestSet] ON 

INSERT [dbo].[OAIHarvestSet] ([HarvestSetID], [HarvestSetName], [SetID], [RepositoryFormatID], [DefaultRecordType], [IsActive], [CreationDate], [LastModifiedDate]) VALUES (1, N'Pensoft - ZooKeys', 4, 6, N'Segment', 1, CAST(0x0000A25300C2E012 AS DateTime), CAST(0x0000A25300C2E012 AS DateTime))
INSERT [dbo].[OAIHarvestSet] ([HarvestSetID], [HarvestSetName], [SetID], [RepositoryFormatID], [DefaultRecordType], [IsActive], [CreationDate], [LastModifiedDate]) VALUES (2, N'SciELO - Brazilian Journal of Medical and Biological Research', 13, 7, N'Segment', 1, CAST(0x0000A25300C556F2 AS DateTime), CAST(0x0000A25300C556F2 AS DateTime))
INSERT [dbo].[OAIHarvestSet] ([HarvestSetID], [HarvestSetName], [SetID], [RepositoryFormatID], [DefaultRecordType], [IsActive], [CreationDate], [LastModifiedDate]) VALUES (3, N'Bibdigital Real Jardin Botanico', NULL, 14, N'BookJournal', 1, CAST(0x0000A25300C59129 AS DateTime), CAST(0x0000A25300C59129 AS DateTime))
SET IDENTITY_INSERT [dbo].[OAIHarvestSet] OFF
