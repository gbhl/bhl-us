
CREATE PROCEDURE [dbo].[NameResolvedSearchForPagesDownload]

@ResolvedNameString nvarchar(100)

AS 

SET NOCOUNT ON

SELECT	t.TitleID,
		i.ItemID,
		p.PageID,
		ISNULL(bl.BibliographicLevelName, '') AS BibliographicLevelName,
		t.FullTitle,
		t.ShortTitle,
		ISNULL(t.PartNumber, '') AS PartNumber,
		ISNULL(t.PartName, '') AS PartName,
		ISNULL(t.Datafield_260_a, '') AS PublisherPlace,
		ISNULL(t.Datafield_260_b, '') AS Publisher,
		ISNULL(COALESCE(NULLIF(p.Year, ''), NULLIF(i.Year, ''), NULLIF(CONVERT(nvarchar(20), StartYear), '')), '') AS [Date],
		ISNULL(n.FullName + ' ', '') +  
				ISNULL(a.Numeration + ' ', '') + ISNULL(a.Unit + ' ', '') +
				ISNULL(a.Title + ' ', '') + ISNULL(a.Location + ' ', '') + 
				ISNULL(n.FullerForm + ' ', '') + ISNULL(a.StartDate  + ' ', '') + 
				CASE WHEN ISNULL(a.StartDate, '') <> '' THEN '-' ELSE '' END + 
				ISNULL(a.EndDate, '') AS Authors,
		ISNULL(i.Volume, '') AS Volume,
		LTRIM(RTRIM(ISNULL(ip.PagePrefix, '') +  ' ' + ISNULL(ip.PageNumber, ''))) AS IndicatedPages,
		CASE WHEN ISNULL(i.CallNumber, '') = '' THEN t.CallNumber ELSE i.CallNumber END AS CallNumber,
		ISNULL(l.LanguageName, '') AS LanguageName
FROM	dbo.NameResolved nr WITH (NOLOCK)
		INNER JOIN dbo.Name nm WITH (NOLOCK) ON nr.NameResolvedID = nm.NameResolvedID
		INNER JOIN dbo.NamePage np WITH (NOLOCK) ON nm.NameID = np.NameID
		INNER JOIN dbo.Page p WITH (NOLOCK) ON np.PageID = p.PageID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON p.itemid = i.itemid
		INNER JOIN dbo.Title t WITH (NOLOCK) ON i.primarytitleid = t.titleid
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON ti.ItemID = i.ItemID AND ti.TitleID = t.TitleID
		LEFT JOIN dbo.BibliographicLevel bl WITH (NOLOCK) ON t.BibliographicLevelID = bl.BibliographicLevelID
		LEFT JOIN dbo.[Language] l WITH (NOLOCK) ON i.LanguageCode = l.LanguageCode
		LEFT JOIN dbo.IndicatedPage ip WITH (NOLOCK) ON p.PageID = ip.PageID AND ip.Sequence = 1
		LEFT JOIN (		SELECT	ta.TitleID, MIN(ta.AuthorID) AS AuthorID
						FROM	dbo.TitleAuthor ta WITH (NOLOCK) INNER JOIN dbo.AuthorRole r WITH (NOLOCK)
									ON ta.AuthorRoleID = r.AuthorRoleID 
									AND r.MarcDataFieldTag IN ('100', '700', '110', '710')
						GROUP BY ta.TitleID ) ta ON t.TitleID = ta.TitleID
		LEFT JOIN dbo.Author a WITH (NOLOCK) ON ta.AuthorID = a.AuthorID AND a.IsActive = 1
		LEFT JOIN dbo.AuthorName n WITH (NOLOCK) ON a.AuthorID = n.AuthorID AND n.IsPreferredName = 1
WHERE	nr.ResolvedNameString = @ResolvedNameString
		AND nm.IsActive = 1
		AND i.ItemStatusID = 40
		AND t.PublishReady = 1
ORDER BY
		t.SortTitle, ti.ItemSequence, p.SequenceOrder



