CREATE PROCEDURE [dbo].[AuthorSelectWithNameByAuthorID]

@AuthorID INT

AS 

SET NOCOUNT ON

SELECT	a.AuthorID,
		a.AuthorTypeID,
		n.FullName,
		a.Numeration,
		a.Title,
		a.Unit,
		a.Location,
		n.FullerForm,
		a.StartDate,
		a.EndDate,
		a.StartDate + CASE WHEN a.StartDate <> '' THEN '-' ELSE '' END + a.EndDate AS Dates,
		a.IsActive,
		a.RedirectAuthorID,
		a.CreationDate,
		a.LastModifiedDate,
		a.CreationUserID,
		a.LastModifiedUserID
FROM	dbo.Author a INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID
WHERE	a.AuthorID = @AuthorID
AND		n.IsPreferredName = 1



