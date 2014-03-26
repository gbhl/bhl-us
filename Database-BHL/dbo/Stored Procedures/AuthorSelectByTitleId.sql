
CREATE PROCEDURE [dbo].[AuthorSelectByTitleId]

@TitleID int

AS 

SET NOCOUNT ON

SELECT	a.AuthorID,
		n.FullName,
		ISNULL(a.StartDate, '') AS StartDate,
		ISNULL(a.EndDate, '') AS EndDate,
		a.Numeration,
		a.Unit,
		a.Title,
		a.Location,
		n.FullerForm,
		ta.AuthorRoleID,
		ta.Relationship,
		ta.TitleOfWork,
		r.MARCDataFieldTag
FROM	dbo.Author a INNER JOIN dbo.AuthorName n
			ON a.AuthorID = n.AuthorID
		INNER JOIN dbo.TitleAuthor ta 
			ON a.AuthorID = ta.AuthorID
		INNER JOIN dbo.Title t 
			ON ta.TitleID = t.TitleID
		INNER JOIN dbo.AuthorRole r
			ON ta.AuthorRoleID = r.AuthorRoleID
WHERE	t.TitleID = @TitleID
AND		a.IsActive = 1
AND		n.IsPreferredName = 1
ORDER BY MARCDataFieldTag, n.FullName + a.Numeration + a.Unit + a.Title + a.Location + n.FullerForm + ta.Relationship + ta.TitleOfWork

GO
