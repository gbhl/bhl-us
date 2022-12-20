CREATE PROCEDURE dbo.AuthorSelectByAuthorID

@AuthorID INT

AS 

SET NOCOUNT ON

SELECT	a.AuthorID,
		a.AuthorTypeID,
		t.AuthorTypeName,
		a.StartDate,
		a.EndDate,
		a.Numeration,
		a.Title,
		a.Unit,
		a.Location,
		a.Note,
		a.IsActive,
		a.RedirectAuthorID,
		a.CreationDate,
		a.LastModifiedDate,
		a.CreationUserID,
		a.LastModifiedUserID
FROM	dbo.Author a
		LEFT JOIN dbo.AuthorType t ON a.AuthorTypeID = t.AuthorTypeID
WHERE	AuthorID = @AuthorID

GO
