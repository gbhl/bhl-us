CREATE PROCEDURE dbo.CurrentStatsSelect

AS

BEGIN

-- Return column names that match C# class attribute names
SELECT		TitleActive AS TitleCount,
			TitleTotal,
			BookActive AS VolumeCount,
			BookTotal AS VolumeTotal,
			PageActive AS [PageCount],
			PageTotal,
			SegmentActive AS SegmentCount,
			SegmentTotal,
			BookSegmentActive AS ItemSegmentCount,
			BookSegmentTotal AS ItemSegmentTotal,
			NameActive AS NameCount,
			NameTotal,
			UniqueNameActive AS UniqueNameCount,
			UniqueNameTotal,
			VerifiedNameActive AS VerifiedNameCount,
			VerifiedNameTotal,
			LastModifiedDate
FROM		dbo.CurrentStats

END

GO
