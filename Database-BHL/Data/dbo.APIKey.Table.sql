SET IDENTITY_INSERT [dbo].[APIKey] ON 

INSERT [dbo].[APIKey] ([ApiKeyID], [ContactName], [EmailAddress], [ApiKeyValue], [IsActive], [CreationDate], [LastModifiedDate]) VALUES (1, N'Test User', N'support@biodiversitylibrary.org', N'12345678-1234-1234-1234-123456789012', 1, CAST(N'2010-02-08 10:37:55.000' AS DateTime), CAST(N'2010-02-08 10:37:55.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[APIKey] OFF
