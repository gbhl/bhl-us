CREATE PROCEDURE [dbo].[ExportAuthor]

AS

BEGIN

SET NOCOUNT ON

SELECT	ta.TitleID,
		ta.SequenceOrder,
		a.AuthorID,
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
		CONVERT(nvarchar(16), ta.CreationDate, 120) AS CreationDate,
		r.MARCDataFieldTag,
		c.HasLocalContent,
		c.HasExternalContent
INTO	#Authors
FROM	dbo.TitleAuthor ta
		INNER JOIN dbo.Title t ON ta.TitleID = t.TitleID
		INNER JOIN dbo.ItemTitle it ON t.TitleID = it.TitleID
		INNER JOIN dbo.Book b ON it.ItemID = b.ItemID
		INNER JOIN dbo.Author a ON ta.AuthorID = a.AuthorID
		INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID AND n.IsPreferredName = 1
		INNER JOIN dbo.AuthorRole r ON ta.AuthorRoleID = r.AuthorRoleID
		INNER JOIN dbo.SearchCatalog c ON ta.TitleID = c.TitleID AND b.BookID = c.ItemID

SELECT	TitleID,
		AuthorID,
		CreatorType,
		CreatorName,
		MIN(CreationDate) AS CreationDate,
		MAX(HasLocalContent) AS HasLocalContent,
		MAX(HasExternalContent) AS HasExternalContent
FROM	#Authors
GROUP BY
		TitleID,
		AuthorID,
		CreatorType,
		CreatorName
ORDER BY 
		TitleID, MIN(SequenceOrder), MIN(MARCDataFieldTag), CreatorName

END

GO
