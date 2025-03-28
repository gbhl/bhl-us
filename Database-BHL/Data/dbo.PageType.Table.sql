SET IDENTITY_INSERT [dbo].[PageType] ON 

INSERT [dbo].[PageType] ([PageTypeID], [PageTypeName], [PageTypeDescription], [Active]) VALUES (1, N'Title Page', N'Title Page', 1)
INSERT [dbo].[PageType] ([PageTypeID], [PageTypeName], [PageTypeDescription], [Active]) VALUES (2, N'Text', N'Page of Text', 1)
INSERT [dbo].[PageType] ([PageTypeID], [PageTypeName], [PageTypeDescription], [Active]) VALUES (3, N'Illustration', N'Page of Illustration', 1)
INSERT [dbo].[PageType] ([PageTypeID], [PageTypeName], [PageTypeDescription], [Active]) VALUES (4, N'Verso', N'Left reverse side', 0)
INSERT [dbo].[PageType] ([PageTypeID], [PageTypeName], [PageTypeDescription], [Active]) VALUES (5, N'Recto', N'Right reverse side', 0)
INSERT [dbo].[PageType] ([PageTypeID], [PageTypeName], [PageTypeDescription], [Active]) VALUES (6, N'Blank', N'Blank', 1)
INSERT [dbo].[PageType] ([PageTypeID], [PageTypeName], [PageTypeDescription], [Active]) VALUES (7, N'Index', N'Index', 1)
INSERT [dbo].[PageType] ([PageTypeID], [PageTypeName], [PageTypeDescription], [Active]) VALUES (8, N'Cover', N'Front or Back Cover', 1)
INSERT [dbo].[PageType] ([PageTypeID], [PageTypeName], [PageTypeDescription], [Active]) VALUES (9, N'Appendix', N'Appendix', 1)
INSERT [dbo].[PageType] ([PageTypeID], [PageTypeName], [PageTypeDescription], [Active]) VALUES (10, N'Map', N'Map', 1)
INSERT [dbo].[PageType] ([PageTypeID], [PageTypeName], [PageTypeDescription], [Active]) VALUES (11, N'Table of Contents', N'Used to denote pages for Table of Contents', 1)
INSERT [dbo].[PageType] ([PageTypeID], [PageTypeName], [PageTypeDescription], [Active]) VALUES (12, N'Article Start', N'Used to denote the beginning page of an article', 0)
INSERT [dbo].[PageType] ([PageTypeID], [PageTypeName], [PageTypeDescription], [Active]) VALUES (13, N'Article End', N'Used to denote the end page of an article', 0)
INSERT [dbo].[PageType] ([PageTypeID], [PageTypeName], [PageTypeDescription], [Active]) VALUES (14, N'Foldout', N'Foldout', 1)
INSERT [dbo].[PageType] ([PageTypeID], [PageTypeName], [PageTypeDescription], [Active]) VALUES (15, N'Issue Start', N'Used to denote the beginning page of an issue', 1)
INSERT [dbo].[PageType] ([PageTypeID], [PageTypeName], [PageTypeDescription], [Active]) VALUES (16, N'Issue End', N'Used to denote the end page of an issue', 0)
INSERT [dbo].[PageType] ([PageTypeID], [PageTypeName], [PageTypeDescription], [Active]) VALUES (17, N'List of Illustrations', N'List of the Illustrations, Plates or FIgures in the book', 1)
INSERT [dbo].[PageType] ([PageTypeID], [PageTypeName], [PageTypeDescription], [Active]) VALUES (18, N'Drawing', N'Page includes a painting, drawing, or diagram', 0)
INSERT [dbo].[PageType] ([PageTypeID], [PageTypeName], [PageTypeDescription], [Active]) VALUES (19, N'Chart', N'Page includes a chart or table', 1)
INSERT [dbo].[PageType] ([PageTypeID], [PageTypeName], [PageTypeDescription], [Active]) VALUES (20, N'Photograph', N'Page includes a photograph', 0)
INSERT [dbo].[PageType] ([PageTypeID], [PageTypeName], [PageTypeDescription], [Active]) VALUES (21, N'Bookplate', N'Page includes a bookplate', 0)
SET IDENTITY_INSERT [dbo].[PageType] OFF
