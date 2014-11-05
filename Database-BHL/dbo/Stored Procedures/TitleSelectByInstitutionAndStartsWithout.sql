CREATE PROCEDURE [dbo].[TitleSelectByInstitutionAndStartsWithout]

@InstitutionCode nvarchar(10),
@StartsWith varchar(1000) = ''

AS 

SET NOCOUNT ON

-- Select titles that are associated with the specified institution, or that
-- have related items which are associated with the specified institution
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
		c.Authors,
		dbo.fnCollectionStringForTitleAndItem(t.TitleID, i.ItemID) AS Collections,
		i.ExternalUrl
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
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID AND i.ItemID = c.ItemID
WHERE	i.ItemStatusID = 40
AND		(t.InstitutionCode = ISNULL(@InstitutionCode, t.InstitutionCode) OR
		 i.InstitutionCode = ISNULL(@InstitutionCode, i.InstitutionCode) )
AND		t.SortTitle NOT LIKE @StartsWith + '%'
ORDER BY 
		t.SortTitle
