SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[TitleKeywordView]
AS
SELECT	pt.TitleID AS PrimaryTitleID,
		b.BookID,
		i.ItemID, 
		b.LanguageCode AS ItemLanguageCode,
		t.TitleID, 
		t.LanguageCode AS TitleLanguageCode,
		t.PublishReady,
		k.KeywordID,
		k.Keyword,
		tk.TitleKeywordID,
		tk.MarcDataFieldTag, tk.MarcSubFieldCode, 
		tk.CreationDate, tk.LastModifiedDate
FROM	dbo.Item i 
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
		INNER JOIN dbo.vwItemPrimaryTitle pt ON i.ItemID = pt.ItemID
		INNER JOIN dbo.ItemTitle it ON i.ItemID = it.ItemID
		INNER JOIN dbo.Title t ON it.TitleID = t.TitleID
		INNER JOIN dbo.TitleKeyword tk ON t.TitleID = tk.TitleID
		INNER JOIN dbo.Keyword k ON tk.KeywordID = k.KeywordID

GO
