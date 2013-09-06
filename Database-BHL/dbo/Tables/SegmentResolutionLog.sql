CREATE TABLE [dbo].[SegmentResolutionLog]
(
	[SegmentResolutionLogID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SegmentID] INT NOT NULL, 
    [MatchingSegmentID] INT NOT NULL, 
    [Score] NUMERIC(9, 8) NULL, 
    [CreationDate] DATETIME NOT NULL DEFAULT GETDATE(), 
    [LastModifiedDate] DATETIME NOT NULL DEFAULT GETDATE(), 
    [CreationUserID] INT NOT NULL DEFAULT 1, 
    [LastModifiedUserID] INT NOT NULL DEFAULT 1
)
