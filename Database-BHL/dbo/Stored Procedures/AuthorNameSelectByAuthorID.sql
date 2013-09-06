
CREATE PROCEDURE [dbo].[AuthorNameSelectByAuthorID]

@AuthorID int

AS 

SET NOCOUNT ON

SELECT	AuthorNameID,
		AuthorID,
		FullName,
		LastName,
		FirstName,
		FullerForm,
		IsPreferredName,
		CreationDate,
		LastModifiedDate,
		CreationUserID,
		LastModifiedUserID
FROM	dbo.AuthorName
WHERE	AuthorID = @AuthorID
ORDER BY
		FullName
		


