SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemType](
	[ItemTypeID] [int] NOT NULL,
	[ItemTypeName] [nvarchar](50) NOT NULL,
	[ItemTypeLabel] [nvarchar](50) NOT NULL,
	[ItemTypeDescription] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_ItemType] PRIMARY KEY CLUSTERED 
(
	[ItemTypeID] ASC
)
)

GO
ALTER TABLE [dbo].[ItemType] ADD  CONSTRAINT [DF_ItemType_Name]  DEFAULT ('') FOR [ItemTypeName]
GO
ALTER TABLE [dbo].[ItemType] ADD  CONSTRAINT [DF_ItemType_Label]  DEFAULT ('') FOR [ItemTypeLabel]
GO
ALTER TABLE [dbo].[ItemType] ADD  CONSTRAINT [DF_ItemType_Description]  DEFAULT ('') FOR [ItemTypeDescription]
GO
