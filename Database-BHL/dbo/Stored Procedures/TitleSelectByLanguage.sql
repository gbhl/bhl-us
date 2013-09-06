
CREATE PROCEDURE [dbo].[TitleSelectByLanguage]

@LanguageCode nvarchar(10) = ''

AS

SET NOCOUNT ON

-- Select titles that are associated with the specified language, or that
-- have related items which are associated with the specified language
SELECT DISTINCT
		t.TitleID,
		i.ItemID,
		t.FullTitle,
		t.SortTitle,
		ISNULL(t.PartNumber, '') AS PartNumber,
		ISNULL(t.PartName, '') AS PartName,
		t.PublicationDetails,
		CASE WHEN ISNULL(i.Year, '') = '' THEN CONVERT(nvarchar(20), t.StartYear) ELSE i.Year END AS [Year],
		t.EditionStatement,
		i.Volume,
		ins.InstitutionName,
		dbo.fnCOinSAuthorStringForTitle(t.TitleID, 0) AS Authors,
		dbo.fnCollectionStringForTitleAndItem(t.TitleID, i.ItemID) AS Collections
FROM	dbo.Title t  WITH (NOLOCK)
		INNER JOIN (
				-- Get the first item for each title
				SELECT	TitleID, MIN(ItemSequence) MinSeq
				FROM	dbo.TitleItem ti WITH (NOLOCK) INNER JOIN dbo.Item itm WITH (NOLOCK)
						ON ti.ItemID = itm.ItemID 
				WHERE	itm.ItemStatusID = 40
				GROUP BY TitleID
				) AS x 
				ON t.TitleID = x.TitleID
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON x.TitleID = ti.TitleID AND x.MinSeq = ti.ItemSequence
		INNER JOIN dbo.Item i WITH (NOLOCK) ON ti.ItemID = i.ItemID
		LEFT OUTER JOIN Institution ins WITH (NOLOCK) ON ins.InstitutionCode = t.InstitutionCode
		LEFT OUTER JOIN dbo.Language l WITH (NOLOCK) ON l.LanguageCode = t.LanguageCode
		LEFT JOIN dbo.TitleLanguage tl WITH (NOLOCK) ON t.TitleID = tl.TitleID
		LEFT JOIN dbo.ItemLanguage il WITH (NOLOCK) ON i.ItemID = il.ItemID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID AND i.ItemID = c.ItemID
WHERE	t.PublishReady = 1
AND		i.ItemStatusID = 40
AND		(t.LanguageCode = @LanguageCode OR
		 i.LanguageCode = @LanguageCode OR
		 ISNULL(tl.LanguageCode, '') = @LanguageCode OR
		 ISNULL(il.LanguageCode, '') = @LanguageCode )
AND		@LanguageCode <> ''
ORDER BY 
		t.SortTitle


