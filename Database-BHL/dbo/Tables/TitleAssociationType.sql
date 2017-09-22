CREATE TABLE [dbo].[TitleAssociationType] (
    [TitleAssociationTypeID] INT           IDENTITY (1, 1) NOT NULL,
    [TitleAssociationName]   NVARCHAR (60) CONSTRAINT [DF_Table_1_AssociationName] DEFAULT ('') NOT NULL,
    [MARCTag]                NVARCHAR (20) CONSTRAINT [DF_TitleAssociationType_MARCTag] DEFAULT ('') NOT NULL,
    [MARCIndicator2]         NCHAR (1)     CONSTRAINT [DF_TitleAssociationType_MARCIndicator2] DEFAULT ('') NOT NULL,
    [TitleAssociationLabel]  NVARCHAR (30) CONSTRAINT [DF_TitleAssociationType_TitleAssociationLabel] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_TitleAssociationType] PRIMARY KEY CLUSTERED ([TitleAssociationTypeID] ASC)
);


GO
