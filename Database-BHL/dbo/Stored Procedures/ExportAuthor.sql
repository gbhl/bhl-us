CREATE PROCEDURE dbo.ExportAuthor

AS

BEGIN

SET NOCOUNT ON

SELECT	ta.TitleID,
		CONVERT(nvarchar(50), 
			CASE 
			WHEN r.RoleDescription LIKE 'Main%Personal%' THEN 'Main - Personal Name'
			WHEN r.RoleDescription LIKE 'Main%Corporate%' THEN 'Main - Corporate Name'
			WHEN r.RoleDescription LIKE 'Main%Meeting%' THEN 'Main - Meeting Name'
			WHEN r.RoleDescription LIKE 'Added%Personal%' THEN 'Added - Personal Name'
			WHEN r.RoleDescription LIKE 'Added%Corporate%' THEN 'Added - Corporate Name'
			WHEN r.RoleDescription LIKE 'Added%Meeting%' THEN 'Added - Meeting Name'
			WHEN r.RoleDescription LIKE 'Added%Uncontrolled%' THEN 'Added - Uncontrolled Name'
			ELSE SUBSTRING(r.RoleDescription, 1, 50) 
			END) AS CreatorType,
		n.FullName + 
			CASE WHEN a.Numeration = '' THEN '' ELSE ' ' + a.Numeration END + 
			CASE WHEN a.Unit = '' THEN '' ELSE ' ' + a.Unit END + 
			CASE WHEN a.Title = '' THEN '' ELSE ' '  + a.Title END + 
			CASE WHEN a.Location = '' THEN '' ELSE ' ' + a.Location END  + 
			CASE WHEN n.FullerForm = '' THEN '' ELSE ' ' + n.FullerForm END + 
			CASE WHEN a.StartDate = '' THEN '' ELSE ' ' + a.StartDate + '-' END + a.EndDate AS CreatorName,
		CONVERT(nvarchar(16), ta.CreationDate, 120) AS CreationDate
FROM	dbo.TitleAuthor ta WITH (NOLOCK)
		INNER JOIN dbo.Author a WITH (NOLOCK) ON ta.AuthorID = a.AuthorID
		INNER JOIN dbo.AuthorName n WITH (NOLOCK) ON a.AuthorID = n.AuthorID AND n.IsPreferredName = 1
		INNER JOIN dbo.Title t WITH (NOLOCK) ON ta.TitleID = t.TitleID AND t.PublishReady = 1 
		INNER JOIN dbo.AuthorRole r WITH (NOLOCK) ON ta.AuthorRoleID = r.AuthorRoleID
ORDER BY 
		ta.TitleID, r.MARCDataFieldTag, n.FullName

END

