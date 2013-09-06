CREATE VIEW [dbo].[TitleKeywordView]
AS
SELECT	i.PrimaryTitleID,
		i.ItemID, 
		i.InstitutionCode AS ItemInstitutionCode, 
		i.LanguageCode AS ItemLanguageCode,
		t.TitleID, 
		t.InstitutionCode AS TitleInstitutionCode,
		t.LanguageCode AS TitleLanguageCode,
		t.PublishReady,
		k.KeywordID,
		k.Keyword,
		tk.TitleKeywordID,
		tk.MarcDataFieldTag, tk.MarcSubFieldCode, 
		tk.CreationDate, tk.LastModifiedDate
FROM	dbo.Item i INNER JOIN dbo.TitleItem ti
			ON i.ItemID = ti.ItemID
		INNER JOIN dbo.Title t
			ON ti.TitleID = t.TitleID
		INNER JOIN dbo.TitleKeyword tk
			ON t.TitleID = tk.TitleID
		INNER JOIN dbo.Keyword k
			ON tk.KeywordID = k.KeywordID

