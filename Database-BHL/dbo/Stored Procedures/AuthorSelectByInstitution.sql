
CREATE PROCEDURE [dbo].[AuthorSelectByInstitution]

@InstitutionCode nvarchar(10) = '',
@LanguageCode nvarchar(10) = ''

AS 

-- @InstitutionCode and @LanguageCode are no longer used (Feb 15, 2013)

SET NOCOUNT ON

SELECT DISTINCT
		a.AuthorID,
		n.FullName,
		a.StartDate,
		a.EndDate
FROM	dbo.Author a 
		INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID
		INNER JOIN dbo.TitleAuthor ta ON a.AuthorID = ta.AuthorID
		INNER JOIN dbo.Title t ON ta.TitleID = t.TitleID
WHERE	a.IsActive = 1
AND		n.IsPreferredName = 1
AND		n.FullName <> ''
AND		t.PublishReady = 1

UNION

SELECT DISTINCT
		a.AuthorID,
		n.FullName,
		a.StartDate,
		a.EndDate
FROM	dbo.Author a 
		INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID
		INNER JOIN dbo.SegmentAuthor sa ON a.AuthorID = sa.AuthorID
		INNER JOIN dbo.Segment s ON sa.SegmentID = s.SegmentID
WHERE	a.IsActive = 1
AND		n.IsPreferredName = 1
AND		n.FullName <> ''
AND		s.SegmentStatusID IN (10, 20)

ORDER BY 
		n.FullName

