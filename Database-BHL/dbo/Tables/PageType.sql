CREATE TABLE [dbo].[PageType] (
    [PageTypeID]          INT            IDENTITY (1, 1) NOT NULL,
    [PageTypeName]        NVARCHAR (30)  NOT NULL,
    [PageTypeDescription] NVARCHAR (255) NULL,
	[Active]              TINYINT NOT NULL CONSTRAINT DF_PageType_Active DEFAULT 1,
	[CreationDate]        DATETIME NULL,
	[LastModifiedDate]    DATETIME NULL,
	[CreationUserID]      INT NULL,
	[LastModifiedUserID]  INT NULL
    CONSTRAINT [PageType_PK] PRIMARY KEY CLUSTERED ([PageTypeID] ASC)
);

GO
