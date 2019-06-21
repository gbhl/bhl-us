CREATE PROCEDURE [dbo].[ApiAuthorSelectByTitleID]

@TitleID int

AS 

SET NOCOUNT ON

SELECT      a.AuthorID ,
            FullName ,
            RoleDescription ,
            a.Numeration,
            a.Unit,
            a.Title,
            a.Location,
            FullerForm,
			ta.Relationship,
			ta.TitleOfWork,
            a.StartDate + CASE WHEN a.StartDate <> '' THEN N'-' ELSE N'' END + a.EndDate AS Dates
FROM  dbo. Author a INNER JOIN dbo. TitleAuthor ta
                   ON a. AuthorID = ta .AuthorID
             INNER JOIN dbo.AuthorName n
                   ON a. AuthorID = n .AuthorID
                   AND n. IsPreferredName = 1
             INNER JOIN dbo.AuthorRole r
                   ON ta. AuthorRoleID = r .AuthorRoleID
             INNER JOIN dbo.Title t
                   ON ta. TitleID = t .TitleID
                   AND t. PublishReady = 1
WHERE t. TitleID = @TitleID
AND		a.IsActive = 1
ORDER BY ta.SequenceOrder, n.FullName, a.Numeration, a.Unit, a.Title, a.Location, n.FullerForm, ta.Relationship, ta.TitleOfWork
