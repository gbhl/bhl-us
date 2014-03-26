CREATE PROCEDURE [dbo].[TitleSimpleSelectByAuthor]

@AuthorId	int

AS

SET NOCOUNT ON

SELECT
	T.TitleID,
	T.FullTitle,
	R.RoleDescription,
	TA.Relationship,
	TA.TitleOfWork
FROM dbo.TitleAuthor TA
	INNER JOIN dbo.Title T ON TA.TitleID = T.TitleID
	INNER JOIN dbo.AuthorRole R ON TA.AuthorRoleID = R.AuthorRoleID
WHERE 
	T.PublishReady = 1 AND
	TA.AuthorID = @AuthorId
ORDER BY T.SortTitle
