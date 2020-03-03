CREATE PROCEDURE dbo.ExportKBART

@UrlRoot nvarchar(200) = 'http://www.biodiversitylibrary.org/bibliography/'

AS

BEGIN

SET NOCOUNT ON

CREATE TABLE #kbart
	(
		publication_title nvarchar(2000) NULL DEFAULT(''),
		print_identifier nvarchar(70) NULL DEFAULT(''),
		online_identifier char(1) NULL DEFAULT(''),
		date_first_issue_online nvarchar(5) NULL DEFAULT(''),
		num_first_vol_online nvarchar(10) NULL DEFAULT(''),
		num_first_issue_online nvarchar(5) NULL DEFAULT(''),
		date_last_issue_online nvarchar(5) NULL DEFAULT(''),
		num_last_vol_online nvarchar(10) NULL DEFAULT(''),
		num_last_issue_online nvarchar(5) NULL DEFAULT(''),
		title_url nvarchar(60) NULL DEFAULT(''),
		first_author_temp nvarchar(100) NULL DEFAULT(''),
		first_author nvarchar(50) NULL DEFAULT(''),
		title_id int NULL,
		embargo_info char(1) NULL DEFAULT(''),
		coverage_depth nvarchar(10) NULL DEFAULT('fulltext'),
		notes nvarchar(100) NULL DEFAULT(''),
		publisher_name nvarchar(255) NULL DEFAULT(''),
		publication_type nvarchar(30) NULL DEFAULT(''),
		date_monograph_published_print nvarchar(5) NULL DEFAULT(''),
		date_monograph_published_online nvarchar(10) NULL DEFAULT(''),
		monograph_volume nvarchar(100) NULL DEFAULT(''),
		monograph_edition nvarchar(450) NULL DEFAULT(''),
		first_editor char(1) NULL DEFAULT(''),
		parent_publication_title_id nvarchar(1) NULL DEFAULT(''),
		preceeding_publication_title_id nvarchar(60) NULL DEFAULT(''),
		access_type char(1) NULL DEFAULT('f'),
		ItemID int NULL,
		ItemSequence int NULL
	)

-- Initial data set
INSERT INTO #kbart
	(
	publication_title,
	title_url,
	title_id,
	notes,
	publisher_name,
	publication_type,
	date_monograph_published_print,
	date_monograph_published_online,
	monograph_volume,
	monograph_edition,
	ItemID,
	ItemSequence
	)
SELECT	t.FullTitle,
		@UrlRoot + CONVERT(varchar(10), t.TitleID),
		t.TitleID,
		CASE WHEN b.MARCCode = 's' THEN 'Coverage information is for a single journal issue.' ELSE '' END,
		ISNULL(t.Datafield_260_b, ''),
		ISNULL(CASE WHEN b.MARCCode = 'm' THEN 'monograph' WHEN b.MARCCode = 's' THEN 'serial' ELSE b.BibliographicLevelName END, ''),
		CASE WHEN b.MARCCode = 'm' THEN ISNULL(CONVERT(varchar(10), t.StartYear), '') ELSE '' END,
		CASE WHEN b.MARCCode = 'm' THEN ISNULL(CONVERT(varchar(20), i.CreationDate, 101), '05-04-2006') ELSE '' END,
		CASE 
			WHEN b.MARCCode = 'm' 
			THEN ISNULL(i.StartVolume, '') + CASE WHEN i.EndVolume <> '' THEN '-' + i.EndVolume ELSE '' END
			ELSE '' 
		END,
		CASE WHEN b.MARCCode = 'm' THEN ISNULL(t.EditionStatement, '') ELSE '' END,
		i.ItemID,
		ti.ItemSequence
FROM	dbo.Title t
		LEFT JOIN dbo.BibliographicLevel b WITH (NOLOCK) ON t.BibliographicLevelID = b.BibliographicLevelID
		INNER JOIN dbo.TitleItem ti ON t.TitleID = ti.TitleID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON ti.ItemID = i.ItemID AND t.TitleID = i.PrimaryTitleID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID AND i.ItemID = c.ItemID
WHERE	t.PublishReady = 1
AND		i.ItemStatusID = 40
AND		c.HasLocalContent = 1
AND		b.MARCCode NOT IN ('c', 'd') -- Omit collections until we decide appropriate 'publication_type' value

UPDATE	#kbart
SET		date_first_issue_online = ISNULL(LEFT(i.Year, 4), ''),
		num_first_vol_online = ISNULL(i.StartVolume, ''),
		num_first_issue_online = ISNULL(CASE WHEN i.StartIssue = '' THEN i.StartNumber ELSE i.StartIssue END, ''),
		date_last_issue_online = ISNULL(CASE WHEN i.EndYear = '' THEN i.Year ELSE i.EndYear END, ''),
		num_last_vol_online = ISNULL(CASE WHEN i.EndVolume = '' THEN i.StartVolume ELSE i.EndVolume END, ''),
		num_last_issue_online = 
			CASE 
				WHEN 
					CASE WHEN i.EndIssue = '' THEN i.StartIssue ELSE i.EndIssue END = ''
				THEN 
					CASE WHEN i.EndNumber = '' THEN i.StartNumber ELSE i.EndNumber END
				ELSE
					CASE WHEN i.EndIssue = '' THEN i.StartIssue ELSE i.EndIssue END
			END
FROM	#kbart k
		INNER JOIN dbo.Item i ON k.ItemID = i.ItemID
WHERE	k.publication_type like 'serial%'

-- Get preceeding title identifiers
UPDATE	#kbart
SET		preceeding_publication_title_id = @UrlRoot + CONVERT(nvarchar(10), a.AssociatedTitleID)
FROM	#kbart k 
		INNER JOIN dbo.TitleAssociation a WITH (NOLOCK) ON k.title_id = a.TitleID
		INNER JOIN dbo.TitleAssociationType tat WITH (NOLOCK) ON a.TitleAssociationTypeID = tat.TitleAssociationTypeID
WHERE	tat.MARCTag = '780'
AND		a.AssociatedTitleID IS NOT NULL

-- Get print identifiers
UPDATE	#kbart
SET		print_identifier = ti.IdentifierValue
FROM	#kbart k
		INNER JOIN dbo.Title_Identifier ti WITH (NOLOCK) ON k.title_id = ti.TitleID
WHERE	ti.IdentifierID IN (2, 3) -- ISSN and ISBN

-- Get first authornames
UPDATE	#kbart
SET		first_author_temp = dbo.fnCOinSGetFirstAuthorNameForTitle(title_id, '100')
WHERE	publication_type = 'monograph'

UPDATE	#kbart
SET		first_author = CASE WHEN CHARINDEX(',', first_author_temp) > 0 THEN LEFT(first_author_temp, CHARINDEX(',', first_author_temp) - 1) ELSE first_author_temp END
WHERE	publication_type = 'monograph'

-- Final result set
SELECT DISTINCT
		publication_title,
		print_identifier,
		online_identifier,
		date_first_issue_online,
		num_first_vol_online,
		num_first_issue_online,
		date_last_issue_online,
		num_last_vol_online,
		num_last_issue_online,
		title_url,
		first_author,
		CONVERT(nvarchar(10), title_id) AS title_id,
		embargo_info,
		coverage_depth,
		notes,
		publisher_name,
		publication_type,
		date_monograph_published_print,
		date_monograph_published_online,
		monograph_volume,
		monograph_edition,
		first_editor,
		parent_publication_title_id,
		preceeding_publication_title_id,
		access_type
FROM	#kbart 
ORDER BY title_id, num_first_vol_online, num_first_issue_online

-- Clean up
DROP TABLE #kbart

END
