SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[TitleAuthorView]
AS
SELECT	pt.TitleID AS PrimaryTitleID, b.BookID, i.ItemID, 
		b.LanguageCode AS ItemLanguageCode,
		t.TitleID, 
		t.LanguageCode AS TitleLanguageCode,
		t.PublishReady,
		a.AuthorID, a.StartDate, a.EndDate, a.IsActive,
		n.FullName, n.FullerForm, n.IsPreferredName,
		a.Numeration, a.Title, a.Unit, a.Location,
		a.CreationDate, a.LastModifiedDate
FROM	dbo.Item i 
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
		INNER JOIN dbo.ItemTitle it ON i.ItemID = it.ItemID
		INNER JOIN dbo.vwItemPrimaryTitle pt ON i.ItemID = pt.ItemID
		INNER JOIN dbo.Title t ON it.TitleID = t.TitleID
		INNER JOIN dbo.TitleAuthor ta ON t.TitleID = ta.TitleID
		INNER JOIN dbo.Author a	ON ta.AuthorID = a.AuthorID
		INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID

GO
