CREATE PROCEDURE dbo.WDEntityIdentifierSelectNeedReview

AS

BEGIN

WITH cte1 AS (
	SELECT DISTINCT
			a1.AuthorID AS OriginalAuthorID,
			COALESCE(a10.AuthorID, a9.AuthorID, a8.AuthorID, a7.AuthorID, a6.AuthorID,
					a5.AuthorID, a4.AuthorID, a3.AuthorID, a2.AuthorID, a1.AuthorID) AS CurrentAuthorID,
			id.IdentifierName, ai.IdentifierValue, ai.CreationDate
	FROM	dbo.BHLAuthorIdentifier ai
			INNER JOIN dbo.BHLIdentifier id ON ai.IdentifierID = id.IdentifierID AND id.IdentifierName = 'Wikidata'
			LEFT JOIN dbo.WDEntityIdentifier ei ON ai.IdentifierValue = ei.IdentifierValue AND ei.IdentifierType = 'Wikidata'
			INNER JOIN dbo.BHLAuthor a1 ON ai.AuthorID = a1.AuthorID
			LEFT JOIN dbo.BHLAuthor a2 ON a1.RedirectAuthorID = a2.AuthorID
			LEFT JOIN dbo.BHLAuthor a3 ON a2.RedirectAuthorID = a3.AuthorID
			LEFT JOIN dbo.BHLAuthor a4 ON a3.RedirectAuthorID = a4.AuthorID
			LEFT JOIN dbo.BHLAuthor a5 ON a4.RedirectAuthorID = a5.AuthorID
			LEFT JOIN dbo.BHLAuthor a6 ON a5.RedirectAuthorID = a6.AuthorID
			LEFT JOIN dbo.BHLAuthor a7 ON a6.RedirectAuthorID = a7.AuthorID
			LEFT JOIN dbo.BHLAuthor a8 ON a7.RedirectAuthorID = a8.AuthorID
			LEFT JOIN dbo.BHLAuthor a9 ON a8.RedirectAuthorID = a9.AuthorID
			LEFT JOIN dbo.BHLAuthor a10 ON a9.RedirectAuthorID = a10.AuthorID
	WHERE	ei.BHLEntityType IS NULL
	),
	cte2 AS (
		SELECT DISTINCT
				t1.TitleID AS OriginalTitleID,
				COALESCE(t10.TitleID, t9.TitleID, t8.TitleID, t7.TitleID, t6.TitleID,
						t5.TitleID, t4.TitleID, t3.TitleID, t2.TitleID, t1.TitleID) AS CurrentTitleID,
				id.IdentifierName, ti.IdentifierValue, ti.CreationDate
		FROM	dbo.BHLTitle_Identifier ti
				INNER JOIN dbo.BHLIdentifier id ON ti.IdentifierID = id.IdentifierID AND id.IdentifierName = 'Wikidata'
				LEFT JOIN dbo.WDEntityIdentifier ei ON ti.IdentifierValue = ei.IdentifierValue AND ei.IdentifierType = 'Wikidata'
				INNER JOIN dbo.BHLTitle t1 ON ti.TitleID = t1.TitleID
				LEFT JOIN dbo.BHLTitle t2 ON t1.RedirectTitleID = t2.TitleID
				LEFT JOIN dbo.BHLTitle t3 ON t2.RedirectTitleID = t3.TitleID
				LEFT JOIN dbo.BHLTitle t4 ON t3.RedirectTitleID = t4.TitleID
				LEFT JOIN dbo.BHLTitle t5 ON t4.RedirectTitleID = t5.TitleID
				LEFT JOIN dbo.BHLTitle t6 ON t5.RedirectTitleID = t6.TitleID
				LEFT JOIN dbo.BHLTitle t7 ON t6.RedirectTitleID = t7.TitleID
				LEFT JOIN dbo.BHLTitle t8 ON t7.RedirectTitleID = t8.TitleID
				LEFT JOIN dbo.BHLTitle t9 ON t8.RedirectTitleID = t9.TitleID
				LEFT JOIN dbo.BHLTitle t10 ON t9.RedirectTitleID = t10.TitleID
		WHERE	ei.BHLEntityType IS NULL
		),
	cte3 AS (
		SELECT	AuthorID 
		FROM	dbo.BHLAuthorIdentifier ai INNER JOIN dbo.BHLIdentifier i ON ai.IdentifierID = i.IdentifierID 
		WHERE	IdentifierName = 'Wikidata' 
		GROUP BY AuthorID HAVING COUNT(*) > 1
		),
	cte4 AS (
		SELECT	IdentifierValue
		FROM	dbo.BHLAuthorIdentifier ai
				INNER JOIN dbo.BHLIdentifier i ON ai.IdentifierID = i.IdentifierID AND i.IdentifierName = 'Wikidata'
				INNER JOIN dbo.BHLAuthor a ON ai.AuthorID = a.AuthorID AND a.IsActive = 1
		GROUP BY IdentifierValue HAVING COUNT(*) > 1
		),
	cte5 AS (
		SELECT	TitleID
		FROM	dbo.BHLTitle_Identifier ti INNER JOIN dbo.BHLIdentifier i ON ti.IdentifierID = i.IdentifierID 
		WHERE	IdentifierName = 'Wikidata' 
		GROUP BY TitleID HAVING COUNT(*) > 1
		),
	cte6 AS (
		SELECT	IdentifierValue
		FROM	dbo.BHLTitle_Identifier ti
				INNER JOIN dbo.BHLIdentifier i ON ti.IdentifierID = i.IdentifierID AND i.IdentifierName = 'Wikidata'
				INNER JOIN dbo.BHLTitle t ON ti.TitleID = t.TitleID AND t.PublishReady = 1
		GROUP BY IdentifierValue HAVING COUNT(*) > 1
		)

-- Invalid Author identifiers (in Wikidata but not BHL)
SELECT	ei.BHLEntityType,
		ei.BHLEntityID,
		'Unknown' AS EntityDescription,
		ei.IdentifierType,
		ei.IdentifierValue,
		'Invalid BHL ' + ei.BHLEntityType + ' ID' AS Message
FROM	dbo.WDEntityIdentifier ei
		LEFT JOIN dbo.BHLAuthor a ON ei.BHLEntityID = a.AuthorID
WHERE	ei.BHLEntityType = 'Author'
AND		ei.IdentifierType = 'Wikidata'
AND		a.AuthorID iS NULL

UNION

-- Invalid Title identifiers (in Wikidata but not BHL)
SELECT	ei.BHLEntityType,
		ei.BHLEntityID,
		'Unknown' AS EntityDescription,
		ei.IdentifierType,
		ei.IdentifierValue,
		'Invalid BHL ' + ei.BHLEntityType + ' ID' AS Message
FROM	dbo.WDEntityIdentifier ei
		LEFT JOIN dbo.BHLTitle t ON ei.BHLEntityID = t.TitleID
WHERE	ei.BHLEntityType = 'Title'
AND		ei.IdentifierType = 'Wikidata'
AND		t.TitleID iS NULL

UNION

-- Wikidata identifiers in BHL, but no corresponding Author ID in Wikidata
SELECT	'Author' AS BHLEntityType,
		cte.CurrentAuthorID AS BHLEntityID, 
		n.FullName + 
			CASE WHEN a.Numeration <> '' THEN ' ' + a.Numeration ELSE '' END + 
			CASE WHEN a.Unit <> '' THEN ' ' + a.Unit ELSE '' END + 
			CASE WHEN a.Title <> '' THEN ' ' + a.Title ELSE '' END + 
			CASE WHEN a.Location <> '' THEN ' ' + a.Location ELSE '' END + 
			CASE WHEN n.FullerForm <> '' THEN ' ' + n.FullerForm ELSE '' END + 
			CASE WHEN a.StartDate <> '' THEN ' ' + a.StartDate ELSE '' END + 
			CASE WHEN ISNULL(a.StartDate, '') <> '' THEN '-' ELSE '' END + a.EndDate + ' [' + t.AuthorTypeName + ']' +
			CASE WHEN cte.OriginalAuthorID <> cte.CurrentAuthorID THEN ' <Redirected from BHL Author ' + CONVERT(varchar(20), cte.OriginalAuthorID) + '>' ELSE '' END +
			CASE WHEN a.IsActive = 0 THEN ' INACTIVE' ELSE '' END AS EntityDescription,
		cte.IdentifierName AS IdentifierType, 
		cte.IdentifierValue,
		'Wikidata identifier in BHL, but corresponding BHL Author ID not in Wikidata' AS Message
FROM	cte1 cte
		INNER JOIN dbo.BHLAuthor a ON cte.CurrentAuthorID = a.AuthorID
		INNER JOIN dbo.BHLAuthorName n ON a.AuthorID = n.AuthorID AND n.IsPreferredName = 1
		INNER JOIN dbo.BHLAuthorType t ON  a.AuthorTypeID = t.AuthorTypeID

UNION

-- Wikidata identifiers in BHL, but no corresponding Title ID in Wikidata
SELECT	'Title' AS BHLEntityType,
		cte.CurrentTitleID AS BHLEntityID, 
		t.FullTitle +
			CASE WHEN cte.OriginalTitleID <> cte.CurrentTitleID THEN ' <Redirected from BHL Title ' + CONVERT(varchar(20), cte.OriginalTitleID) + '>' ELSE '' END +
			CASE WHEN t.PublishReady = 0 THEN ' INACTIVE' ELSE '' END AS EntityDescription,
		cte.IdentifierName AS IdentifierType, 
		cte.IdentifierValue,
		'Wikidata identifier in BHL, but corresponding BHL Title ID not in Wikidata' AS Message
FROM	cte2 cte
		INNER JOIN dbo.BHLTitle t ON cte.CurrentTitleID = t.TitleID

UNION

-- Authors with > 1 Wikidata Identifier
SELECT	'Author' AS BHLEntityType,
		a.AuthorID AS BHLEntityID, 
		n.FullName + 
			CASE WHEN a.Numeration <> '' THEN ' ' + a.Numeration ELSE '' END + 
			CASE WHEN a.Unit <> '' THEN ' ' + a.Unit ELSE '' END + 
			CASE WHEN a.Title <> '' THEN ' ' + a.Title ELSE '' END + 
			CASE WHEN a.Location <> '' THEN ' ' + a.Location ELSE '' END + 
			CASE WHEN n.FullerForm <> '' THEN ' ' + n.FullerForm ELSE '' END + 
			CASE WHEN a.StartDate <> '' THEN ' ' + a.StartDate ELSE '' END + 
			CASE WHEN ISNULL(a.StartDate, '') <> '' THEN '-' ELSE '' END + a.EndDate + ' [' + t.AuthorTypeName + ']' AS EntityDescription,
		i.IdentifierName AS IdentifierType,
		ai.IdentifierValue,
		'BHL Author has more than one Wikidata identifier' AS Message
FROM	dbo.BHLAuthor a
		INNER JOIN cte3 cte ON a.AuthorID = cte.AuthorID
		INNER JOIN dbo.BHLAuthorName n ON a.authorid = n.authorid AND n.IsPreferredName = 1
		INNER JOIN dbo.BHLAuthorType t ON a.AuthorTypeID = t.AuthorTypeID
		INNER JOIN dbo.BHLAuthorIdentifier ai ON cte.AuthorID = ai.AuthorID 
		INNER JOIN dbo.BHLIdentifier i ON ai.IdentifierID = i.IdentifierID AND i.IdentifierName = 'Wikidata'
WHERE	a.IsActive = 1

UNION

-- Wikidata identifiers associated with more than one author
SELECT	'Author' AS BHLEntityType,
		a.AuthorID AS BHLEntityID, 
		n.FullName + 
			CASE WHEN a.Numeration <> '' THEN ' ' + a.Numeration ELSE '' END + 
			CASE WHEN a.Unit <> '' THEN ' ' + a.Unit ELSE '' END + 
			CASE WHEN a.Title <> '' THEN ' ' + a.Title ELSE '' END + 
			CASE WHEN a.Location <> '' THEN ' ' + a.Location ELSE '' END + 
			CASE WHEN n.FullerForm <> '' THEN ' ' + n.FullerForm ELSE '' END + 
			CASE WHEN a.StartDate <> '' THEN ' ' + a.StartDate ELSE '' END + 
			CASE WHEN ISNULL(a.StartDate, '') <> '' THEN '-' ELSE '' END + a.EndDate + ' [' + t.AuthorTypeName + ']' AS EntityDescription,
		i.IdentifierName AS IdentifierType,
		ai.IdentifierValue,
		'Wikidata identifier associated with more than one BHL author' AS Message
FROM	cte4 cte
		INNER JOIN dbo.BHLAuthorIdentifier ai ON cte.IdentifierValue = ai.IdentifierValue
		INNER JOIN dbo.BHLIdentifier i ON ai.IdentifierID = i.IdentifierID AND i.IdentifierName = 'Wikidata'
		INNER JOIN dbo.BHLAuthor a ON ai.AuthorID = a.AuthorID AND a.IsActive = 1
		INNER JOIN dbo.BHLAuthorName n ON ai.AuthorID = n.AuthorID AND n.IsPreferredName = 1
		INNER JOIN dbo.BHLAuthorType t ON a.AuthorTypeID = t.AuthorTypeID

UNION

-- Titles with > 1 Wikidata Identifier
SELECT	'Title' AS BHLEntityType,
		t.TitleID AS BHLEntityID, 
		t.FullTitle COLLATE SQL_Latin1_General_CP1_CI_AI AS EntityDescription,
		i.IdentifierName AS IdentifierType,
		ti.IdentifierValue,
		'BHL Title has more than one Wikidata identifier' AS Message
FROM	dbo.BHLTitle t
		INNER JOIN cte5 cte ON t.TitleID = cte.TitleID
		INNER JOIN dbo.BHLTitle_Identifier ti ON cte.TitleID = ti.TitleID
		INNER JOIN dbo.BHLIdentifier i ON ti.IdentifierID = i.IdentifierID AND i.IdentifierName = 'Wikidata'
WHERE	t.PublishReady = 1

UNION

-- Wikidata identifiers associated with more than one title
SELECT	'Title' AS BHLEntityType,
		t.TitleID AS BHLEntityID, 
		t.FullTitle COLLATE SQL_Latin1_General_CP1_CI_AI AS EntityDescription,
		i.IdentifierName AS IdentifierType,
		ti.IdentifierValue,
		'Wikidata identifier associated with more than one BHL title' AS Message
FROM	cte6 cte
		INNER JOIN dbo.BHLTitle_Identifier ti ON cte.IdentifierValue = ti.IdentifierValue
		INNER JOIN dbo.BHLIdentifier i ON ti.IdentifierID = i.IdentifierID AND i.IdentifierName = 'Wikidata'
		INNER JOIN dbo.BHLTitle t ON ti.TitleID = t.TitleID AND t.PublishReady = 1

ORDER BY BHLEntityType, Message, BHLEntityID

END
GO
