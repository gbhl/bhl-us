
CREATE PROCEDURE [dbo].[ApiAuthorSelectNameStartsWith]

@FullName nvarchar(255)

AS 

SET NOCOUNT ON

SELECT DISTINCT
		a.AuthorID ,
		FullName ,
		a.Numeration,
		a.Unit,
		a.Title,
		a.Location,
		n.FullerForm,
		a.StartDate + CASE WHEN a.StartDate <> '' THEN '-' ELSE '' END + a.EndDate AS Dates
FROM	dbo.Author a INNER JOIN dbo.TitleAuthor ta
			ON a.AuthorID = ta.AuthorID
		INNER JOIN dbo.AuthorName n
			ON a.AuthorID = n.AuthorID
		INNER JOIN dbo.AuthorRole r
			ON ta.AuthorRoleID = r.AuthorRoleID
		INNER JOIN dbo.Title t
			ON ta.TitleID = t.TitleID
			AND t.PublishReady = 1
WHERE	n.FullName LIKE (@FullName + '%')
AND		a.IsActive = 1
ORDER BY n.FullName



