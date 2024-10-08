USE [BHLImport]
GO
INSERT [dbo].[ImportStatus] ([ImportStatusID], [Status], [Description], [CreatedDate], [LastModifiedDate]) VALUES (10, N'New', N'Newly imported data.  Not promoted to production.', CAST(0x00009A12010C0A9B AS DateTime), CAST(0x00009A12010C0A9B AS DateTime))
INSERT [dbo].[ImportStatus] ([ImportStatusID], [Status], [Description], [CreatedDate], [LastModifiedDate]) VALUES (20, N'Complete', N'Data has been promoted to production.', CAST(0x00009A1700A5E9A3 AS DateTime), CAST(0x00009A1700A5E9A3 AS DateTime))
INSERT [dbo].[ImportStatus] ([ImportStatusID], [Status], [Description], [CreatedDate], [LastModifiedDate]) VALUES (99, N'Error', N'There was an error promoting the data to production.', CAST(0x00009A1700A5F904 AS DateTime), CAST(0x00009A1700A5F904 AS DateTime))
