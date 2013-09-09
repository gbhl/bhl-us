CREATE TABLE [dbo].[ImportSourceItemSource] (
    [ImportSourceID]  INT NOT NULL,
    [BHLItemSourceID] INT NOT NULL,
    CONSTRAINT [PK_ImportSourceItemSource] PRIMARY KEY CLUSTERED ([ImportSourceID] ASC, [BHLItemSourceID] ASC)
);

