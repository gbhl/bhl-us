
CREATE VIEW [dbo].[TitleAuthorView]

AS

SELECT	i.PrimaryTitleID, i.ItemID, 
		i.InstitutionCode AS ItemInstitutionCode, 
		i.LanguageCode AS ItemLanguageCode,
		t.TitleID, 
		t.InstitutionCode AS TitleInstitutionCode,
		t.LanguageCode AS TitleLanguageCode,
		t.PublishReady,
		a.AuthorID, a.StartDate, a.EndDate, a.IsActive,
		n.FullName, n.FullerForm, n.IsPreferredName,
		a.Numeration, a.Title, a.Unit, a.Location,
		a.CreationDate, a.LastModifiedDate
FROM	dbo.Item i INNER JOIN dbo.TitleItem ti ON i.ItemID = ti.ItemID
		INNER JOIN dbo.Title t ON ti.TitleID = t.TitleID
		INNER JOIN dbo.TitleAuthor ta ON t.TitleID = ta.TitleID
		INNER JOIN dbo.Author a	ON ta.AuthorID = a.AuthorID
		INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID



