CREATE PROCEDURE [dbo].[TitleAuthorSelectByTitle]

@TitleID INT

AS 

SET NOCOUNT ON

SELECT	TA.TitleAuthorID,
		TA.TitleID,
		TA.AuthorID,
		N.FullName,
		N.FullerForm,
		A.Numeration,
		A.Title,
		A.Unit,
		A.Location,
		TA.AuthorRoleID,
		R.RoleDescription,
		TA.Relationship,
		TA.TitleOfWork 
FROM	dbo.TitleAuthor TA
		INNER JOIN AuthorRole R ON TA.AuthorRoleID = R.AuthorRoleID
		INNER JOIN Author A ON A.AuthorID = TA.AuthorID
		INNER JOIN AuthorName N ON A.AuthorID = N.AuthorID
WHERE	TA.TitleID = @TitleID
AND		A.IsActive = 1
AND		N.IsPreferredName = 1
ORDER BY R.MARCDataFieldTag, N.FullName, FullerForm, Numeration, Unit, Title, Location
