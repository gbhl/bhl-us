CREATE TABLE dbo.IAItemIdentifier
(
	ItemIdentifierID INT IDENTITY(1,1) NOT NULL,
	ItemID INT NOT NULL,
	IdentifierDescription nvarchar(100) NOT NULL CONSTRAINT [DF_ItemIdentifier_IdentifierDescription] DEFAULT (''),
	IdentifierValue nvarchar(125) NOT NULL CONSTRAINT [DF_ItemIdentifier_IdentifierValue] DEFAULT (''),
	CreatedDate datetime NOT NULL CONSTRAINT [DF_ItemIdentifier_CreatedDate] DEFAULT (GETDATE()),
	LastModifiedDate datetime NOT NULL CONSTRAINT [DF_ItemIdentifier_LastModifiedDate] DEFAULT (GETDATE())
	CONSTRAINT [PK_ItemIdentifier] PRIMARY KEY CLUSTERED ( ItemIdentifierID )
)
GO

ALTER TABLE dbo.IAItemIdentifier WITH CHECK 
	ADD CONSTRAINT FK_ItemIdentifier_Item FOREIGN KEY(ItemID) REFERENCES dbo.IAItem (ItemID)
GO

ALTER TABLE dbo.IAItemIdentifier CHECK CONSTRAINT FK_ItemIdentifier_Item
GO

