CREATE TABLE [dbo].[SubjectExclusion] (
    [Code]    NCHAR (1)     CONSTRAINT [DF_SubjectExclusion_Code] DEFAULT ('') NOT NULL,
    [TagText] NVARCHAR (50) CONSTRAINT [DF_SubjectExclusion_TagText] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_SubjectExclusion] PRIMARY KEY CLUSTERED ([Code] ASC, [TagText] ASC)
);

