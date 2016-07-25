CREATE PROCEDURE [dbo].[TitleSelectByInstitutionAndStartsWithout]

@InstitutionCode nvarchar(10),
@StartsWith varchar(1000) = ''

AS 

SET NOCOUNT ON

-- Select titles that are associated with the specified institution, or that
-- have related items which are associated with the specified institution
SELECT DISTINCT
		t.TitleID,
		t.FullTitle,
		t.SortTitle,
		ISNULL(t.PartNumber, '') AS PartNumber,
		ISNULL(t.PartName, '') AS PartName,
		t.PublicationDetails,
		t.StartYear,
		t.EditionStatement,
		c.TitleContributors AS InstitutionName
INTO	#Titles
FROM	dbo.Title t  WITH (NOLOCK)
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON t.TitleID = ti.TitleID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON ti.ItemID = i.ItemID
		INNER JOIN dbo.ItemInstitution ii WITH (NOLOCK) ON i.ItemID = ii.ItemID
		INNER JOIN dbo.InstitutionRole r WITH (NOLOCK) ON ii.InstitutionRoleID = r.InstitutionRoleID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID AND i.ItemID = c.ItemID
WHERE	i.ItemStatusID = 40
AND		ii.InstitutionCode = ISNULL(@InstitutionCode, ii.InstitutionCode)
AND		t.SortTitle NOT LIKE @StartsWith + '%'
AND		r.InstitutionRoleName = 'Contributor'

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
		t.InstitutionName,
		c.Authors,
		dbo.fnCollectionStringForTitleAndItem(t.TitleID, i.ItemID) AS Collections,
		i.ExternalUrl
FROM	#Titles t  WITH (NOLOCK)
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
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID AND i.ItemID = c.ItemID
WHERE	i.ItemStatusID = 40
ORDER BY 
		t.SortTitle

DROP TABLE #Titles
