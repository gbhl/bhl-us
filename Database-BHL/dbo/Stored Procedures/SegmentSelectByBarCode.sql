CREATE PROCEDURE [dbo].[SegmentSelectByBarCode]

@BarCode nvarchar(200)

AS 

SET NOCOUNT ON

SELECT	s.SegmentID,
		s.BookID,
		s.ItemID,
		s.BarCode,
		s.SegmentStatusID,
		s.SequenceOrder,
		s.SegmentGenreID,
		s.Title,
		s.SortTitle,
		s.TranslatedTitle,
		s.ContainerTitle,
		s.PublicationDetails,
		s.PublisherName,
		s.PublisherPlace,
		s.Notes,
		s.Volume,
		s.Series,
		s.Issue,
		s.Date,
		CASE
		WHEN s.PageRange <> '' THEN s.PageRange 
		WHEN s.StartPageNumber <> '' AND s.EndPageNumber <> '' THEN s.StartPageNumber + '--' + s.EndPageNumber
		WHEN s.StartPageNumber <> '' THEN s.StartPageNumber
		ELSE s.EndPageNumber
		END AS PageRange,
		s.StartPageNumber,
		s.EndPageNumber,
		s.StartPageID,
		s.LanguageCode,
		s.Url,
		s.DownloadUrl,
		s.RightsStatus,
		s.RightsStatement,
		s.LicenseName,
		s.LicenseUrl,
		s.CreationDate,
		s.LastModifiedDate,
		s.CreationUserID,
		s.LastModifiedUserID
FROM	dbo.vwSegment s 
WHERE	s.BarCode = @BarCode
ORDER BY
		s.SequenceOrder

GO
