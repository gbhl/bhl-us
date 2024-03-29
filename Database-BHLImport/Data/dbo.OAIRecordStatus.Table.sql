USE [BHLImport]
GO
INSERT [dbo].[OAIRecordStatus] ([OAIRecordStatusID], [RecordStatus], [StatusDescription]) VALUES (10, N'New', N'Newly harvested data.  Not promoted to production.')
INSERT [dbo].[OAIRecordStatus] ([OAIRecordStatusID], [RecordStatus], [StatusDescription]) VALUES (20, N'Complete', N'Data has been promoted to production.')
INSERT [dbo].[OAIRecordStatus] ([OAIRecordStatusID], [RecordStatus], [StatusDescription]) VALUES (30, N'Delete Not Found', N'A record marked for deletion could not be located in production.')
INSERT [dbo].[OAIRecordStatus] ([OAIRecordStatusID], [RecordStatus], [StatusDescription]) VALUES (50, N'On Hold', N'Newly harvested data that will not be promoted to production.')
INSERT [dbo].[OAIRecordStatus] ([OAIRecordStatusID], [RecordStatus], [StatusDescription]) VALUES (99, N'Error', N'There was an error promoting the data to production.')
