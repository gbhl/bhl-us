INSERT [dbo].[DOIStatus] ([DOIStatusID], [DOIStatusName], [DOIStatusDescription]) VALUES (30, N'Queued', N'DOI has been queued for submission to CrossRef to register the DOI or to update metadata.')
INSERT [dbo].[DOIStatus] ([DOIStatusID], [DOIStatusName], [DOIStatusDescription]) VALUES (50, N'Submitted', N'DOI has been submitted to CrossRef.  Awaiting response from CrossRef.')
INSERT [dbo].[DOIStatus] ([DOIStatusID], [DOIStatusName], [DOIStatusDescription]) VALUES (80, N'Error', N'An error condition has prevented approval by CrossRef.')
INSERT [dbo].[DOIStatus] ([DOIStatusID], [DOIStatusName], [DOIStatusDescription]) VALUES (100, N'DOI Approved', N'New DOI has been created OR an existing DOI''s metadata has been updated, submitted to CrossRef, and approved by CrossRef.')
INSERT [dbo].[DOIStatus] ([DOIStatusID], [DOIStatusName], [DOIStatusDescription]) VALUES (200, N'External DOI', N'DOI was not assigned by BHL.  Cannot be modified.')
