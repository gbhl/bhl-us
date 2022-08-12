CREATE PROCEDURE [dbo].[AuthorSelectByNameLikePaged]

@StartsWith nvarchar(255),
@PageNum int = 1,
@NumRows int = 100,
@TotalAuthors int OUTPUT

AS 

BEGIN

SET NOCOUNT ON

SELECT @TotalAuthors = COUNT(DISTINCT AuthorID) FROM (
	SELECT	a.AuthorID
	FROM	dbo.AuthorName n WITH (NOLOCK)
			INNER JOIN dbo.Author a WITH (NOLOCK) ON n.AuthorID = a.AuthorID
			INNER JOIN dbo.TitleAuthor ta WITH (NOLOCK) ON a.AuthorID = ta.AuthorID
			INNER JOIN dbo.Title t WITH (NOLOCK) ON ta.TitleID = t.TitleID
	WHERE	a.IsActive = 1
	AND		n.IsPreferredName = 1
	AND		t.PublishReady = 1
	AND		n.FullName LIKE (@StartsWith + '%')
	UNION
	SELECT	a.AuthorID
	FROM	dbo.Author a WITH (NOLOCK)
			INNER JOIN dbo.AuthorName n WITH (NOLOCK) ON a.AuthorID = n.AuthorID
			INNER JOIN dbo.ItemAuthor ia WITH (NOLOCK) ON a.AuthorID = ia.AuthorID
			INNER JOIN dbo.Item i WITH (NOLOCK) ON ia.ItemID = i.ItemID
			INNER JOIN dbo.Segment s WITH (NOLOCK) ON i.ItemID = s.ItemID
	WHERE	i.ItemStatusID IN (30, 40)
	AND		a.IsActive = 1
	AND		n.IsPreferredName = 1
	AND		n.FullName LIKE (@StartsWith + '%')
	) x

CREATE TABLE #Author (AuthorID int NOT NULL, Sort nvarchar(max) NOT NULL)

INSERT		#Author
SELECT		a.AuthorID,
			ISNULL(n.FullName, '') + ISNULL(Numeration, '') + ISNULL(Unit, '') + ISNULL(a.Title, '') +
			ISNULL(Location, '') + ISNULL(n.FullerForm, '') + ISNULL(StartDate,  '') + ISNULL(EndDate, '') AS Sort
FROM		dbo.AuthorName n WITH (NOLOCK)
			INNER JOIN dbo.Author a WITH (NOLOCK) ON n.AuthorID = a.AuthorID
			INNER JOIN dbo.TitleAuthor ta WITH (NOLOCK) ON a.AuthorID = ta.AuthorID
			INNER JOIN dbo.Title t WITH (NOLOCK) ON ta.TitleID = t.TitleID
WHERE		a.IsActive = 1
AND			n.IsPreferredName = 1
AND			t.PublishReady = 1
AND			n.FullName LIKE (@StartsWith + '%')
UNION
SELECT		a.AuthorID,
			ISNULL(n.FullName, '') + ISNULL(Numeration, '') + ISNULL(Unit, '') + ISNULL(a.Title, '') +
			ISNULL(Location, '') + ISNULL(n.FullerForm, '') + ISNULL(StartDate,  '') + ISNULL(EndDate, '') AS Sort
FROM		dbo.Author a WITH (NOLOCK) 
			INNER JOIN dbo.AuthorName n WITH (NOLOCK) ON a.AuthorID = n.AuthorID
			INNER JOIN dbo.ItemAuthor ia WITH (NOLOCK) ON a.AuthorID = ia.AuthorID
			INNER JOIN dbo.Item i WITH (NOLOCK) ON ia.ItemID = i.ItemID
			INNER JOIN dbo.Segment s WITH (NOLOCK) ON i.ItemID = s.ItemID
WHERE		i.ItemStatusID IN (30, 40)
AND			a.IsActive = 1
AND			n.IsPreferredName = 1
AND			n.FullName LIKE (@StartsWith + '%')
ORDER BY	Sort
OFFSET		@NumRows * (@PageNum - 1) ROWS
FETCH NEXT	@NumRows ROWS ONLY OPTION (RECOMPILE)

SELECT	a.AuthorID,
		n.FullName,
		a.Numeration,
		a.Unit,
		a.Title,
		a.Location,
		a.StartDate + CASE WHEN a.StartDate <> '' THEN '-' ELSE '' END + a.EndDate AS Dates,
		n.FullerForm,
		a.StartDate,
		a.EndDate,
		a.IsActive
FROM	#Author t 
		INNER JOIN dbo.Author a WITH (NOLOCK) ON t.AuthorID = a.AuthorID
		INNER JOIN dbo.AuthorName n WITH (NOLOCK) ON a.AuthorID = n.AuthorID
WHERE	n.IsPreferredName = 1
ORDER BY
		FullName,
		Numeration,
		Unit,
		Title,
		Location,
		FullerForm,
		StartDate, 
		EndDate

END

GO
