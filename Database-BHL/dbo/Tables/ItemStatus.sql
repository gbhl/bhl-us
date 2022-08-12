CREATE TABLE [dbo].[ItemStatus] (
    [ItemStatusID]   INT           NOT NULL,
    [ItemStatusName] NVARCHAR(50)  NOT NULL,
	[ItemStatusDescription] NVARCHAR(MAX) CONSTRAINT [DF_ItemStatus_ItemStatusDescription]  DEFAULT ('') NOT NULL,
	[CreationDate] DATETIME CONSTRAINT [DF_ItemStatus_CreationDate]  DEFAULT (getdate()) NOT NULL,
	[LastModifiedDate] DATETIME CONSTRAINT [DF_ItemStatus_LastModifiedDate]  DEFAULT (getdate()) NOT NULL,
	[CreationUserID] INT CONSTRAINT [DF_ItemStatus_CreationUserID]  DEFAULT ((1)) NOT NULL,
	[LastModifiedUserID] INT CONSTRAINT [DF_ItemStatus_LastModifiedUserID]  DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_ItemStatus] PRIMARY KEY CLUSTERED ([ItemStatusID] ASC)
);

GO
