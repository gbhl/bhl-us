USE [BHLImport]
GO

INSERT dbo.BSSegmentStatus (SegmentStatusID, StatusName, StatusLabel, Description) VALUES (10, N'Harvested', N'Harvested', N'Article metadata harvested.  Ready to load into production tables.')
INSERT dbo.BSSegmentStatus (SegmentStatusID, StatusName, StatusLabel, Description) VALUES (20, N'Published', N'Published', N'Article has been successfully loaded into the production tables.')
INSERT dbo.BSSegmentStatus (SegmentStatusID, StatusName, StatusLabel, Description) VALUES (90, N'SkippedDOI', N'Skipped - DOI Exists', N'Article not loaded into the production tables.  A matching production article exists and has been assigned a BHL-minted DOI.  Updates are not allowed.')
INSERT dbo.BSSegmentStatus (SegmentStatusID, StatusName, StatusLabel, Description) VALUES (91, N'PublishError', N'Publish Error', N'An error occurred while publishing the article to production.  If possible, correct the error and set the SegmentStatus of the segment to "Harvested".')
