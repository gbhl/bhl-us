CREATE TABLE [dbo].[SubjectSample] (
    [SampleID]   INT           IDENTITY (1, 1) NOT NULL,
    [TagText]    NVARCHAR (50) NOT NULL,
    [Count]      INT           NOT NULL,
    [SampleDate] DATETIME      NOT NULL,
    CONSTRAINT [PK__SubjectSample__467D75B8] PRIMARY KEY CLUSTERED ([SampleID] ASC)
);

