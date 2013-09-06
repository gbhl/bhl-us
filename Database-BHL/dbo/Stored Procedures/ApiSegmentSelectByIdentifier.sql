﻿
CREATE PROCEDURE [dbo].[ApiSegmentSelectByIdentifier]

@IdentifierName nvarchar(40),
@IdentifierValue nvarchar(125)

AS 

BEGIN

SET NOCOUNT ON

SELECT	s.SegmentID,
		s.ItemID,
		s.ContributorCode,
		s.ContributorSegmentID,
		s.SequenceOrder,
		inst.InstitutionName AS ContributorName,
		s.SegmentGenreID,
		g.GenreName,
		s.Title,
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
		l.LanguageName,
		s.Url,
		s.DownloadUrl,
		s.RightsStatus,
		s.RightsStatement,
		s.LicenseName,
		s.LicenseUrl,
		REPLACE(scs.Authors, '|', ';') AS Authors,
		REPLACE(scs.Subjects, '|', ';') AS Keywords,
		s.ContributorCreationDate,
		s.ContributorLastModifiedDate,
		s.CreationDate,
		s.LastModifiedDate,
		s.CreationUserID,
		s.LastModifiedUserID
FROM	dbo.Segment s 
		INNER JOIN dbo.SegmentIdentifier si ON s.SegmentID = si.SegmentID
		INNER JOIN dbo.Identifier i ON si.IdentifierID = i.IdentifierID
		LEFT JOIN dbo.Institution inst ON s.ContributorCode = inst.InstitutionCode
		INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
		LEFT JOIN dbo.Language l ON s.LanguageCode = l.LanguageCode
		INNER JOIN dbo.SegmentStatus st ON s.SegmentStatusID = st.SegmentStatusID
		INNER JOIN dbo.SearchCatalogSegment scs on s.SegmentID = scs.SegmentID
WHERE	i.IdentifierName = @IdentifierName
AND		si.IdentifierValue = @IdentifierValue
AND		s.SegmentStatusID IN (10, 20) -- New, Published
ORDER BY
		s.ItemID,
		s.SequenceOrder

END




