SET IDENTITY_INSERT [dbo].[BibliographicLevel] ON 

INSERT [dbo].[BibliographicLevel] ([BibliographicLevelID], [BibliographicLevelName], [BibliographicLevelLabel], [MARCCode]) VALUES (1, N'Monographic component part', N'Book', N'a')
INSERT [dbo].[BibliographicLevel] ([BibliographicLevelID], [BibliographicLevelName], [BibliographicLevelLabel], [MARCCode]) VALUES (2, N'Serial component part', N'Journal', N'b')
INSERT [dbo].[BibliographicLevel] ([BibliographicLevelID], [BibliographicLevelName], [BibliographicLevelLabel], [MARCCode]) VALUES (3, N'Collection', N'Collection', N'c')
INSERT [dbo].[BibliographicLevel] ([BibliographicLevelID], [BibliographicLevelName], [BibliographicLevelLabel], [MARCCode]) VALUES (4, N'Monograph/Item', N'Book', N'm')
INSERT [dbo].[BibliographicLevel] ([BibliographicLevelID], [BibliographicLevelName], [BibliographicLevelLabel], [MARCCode]) VALUES (5, N'Serial', N'Journal', N's')
INSERT [dbo].[BibliographicLevel] ([BibliographicLevelID], [BibliographicLevelName], [BibliographicLevelLabel], [MARCCode]) VALUES (6, N'Subunit', N'Collection', N'd')
INSERT [dbo].[BibliographicLevel] ([BibliographicLevelID], [BibliographicLevelName], [BibliographicLevelLabel], [MARCCode]) VALUES (7, N'Integrating resource', N'Book', N'i')
SET IDENTITY_INSERT [dbo].[BibliographicLevel] OFF
