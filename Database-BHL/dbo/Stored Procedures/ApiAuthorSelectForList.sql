CREATE PROCEDURE [dbo].[ApiAuthorSelectForList]

@IDs dbo.SearchIDTable READONLY

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
FROM	@IDs id INNER JOIN dbo.Author a 
			ON id.ID = a.AuthorID
		INNER JOIN dbo.AuthorName n
			ON a.AuthorID = n.AuthorID
WHERE	a.IsActive = 1
AND		n.IsPreferredName = 1
ORDER BY n.FullName

