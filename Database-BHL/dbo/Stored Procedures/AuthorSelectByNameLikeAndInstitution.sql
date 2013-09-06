
CREATE PROCEDURE [dbo].[AuthorSelectByNameLikeAndInstitution]

@FullName nvarchar(255),
@InstitutionCode nvarchar(10) = '',
@LanguageCode nvarchar(10) = ''

AS 

-- @InstitutionCode and @LanguageCode are no longer used (Feb 15, 2013)

SET NOCOUNT ON

SELECT DISTINCT
		AuthorID
INTO	#tmpAuthor
FROM	dbo.AuthorName
WHERE	FullName LIKE (@FullName + '%')

SELECT DISTINCT
		v.AuthorID,
		v.FullName, 
		v.Numeration,
		v.Unit,
		v.Title,
		v.Location,
		v.StartDate, 
		v.EndDate,
		v.FullerForm
FROM	dbo.TitleAuthorView v 
WHERE	v.AuthorID IN (SELECT AuthorID FROM #tmpAuthor)
AND		v.PublishReady = 1
AND		v.IsActive = 1
AND		v.IsPreferredName = 1

UNION

SELECT DISTINCT
		a.AuthorID,
		n.FullName,
		a.Numeration,
		a.Unit,
		a.Title,
		a.Location,
		a.StartDate,
		a.EndDate,
		n.FullerForm
FROM	dbo.Author a 
		INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID
		INNER JOIN dbo.SegmentAuthor sa ON a.AuthorID = sa.AuthorID
		INNER JOIN dbo.Segment s ON sa.SegmentID = s.SegmentID
WHERE	a.AuthorID IN (SELECT AuthorID FROM #tmpAuthor)
AND		a.IsActive = 1
AND		n.IsPreferredName = 1
AND		s.SegmentStatusID IN (10, 20)
		
ORDER BY v.FullName,
		v.Numeration,
		v.Unit,
		v.Title,
		v.Location,
		v.FullerForm,
		v.StartDate, 
		v.EndDate

