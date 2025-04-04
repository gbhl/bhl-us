CREATE PROCEDURE [dbo].[InstitutionSelectDOIStats]

@SortBy int = 1,
@BHLOnly int = 1

AS
/*------------------------------------------------------------------
 * Returns a list of institutions, including the number
 * of titles and segments with assigned DOIs that each 
 * institution has contributed to. 
 *
 * @SortBy = 1  => Order result set by Institution Name
 * @SortBy = 2  => Order result set by total number of title DOIs
 * @SortBy = 3  => Order result set by total number of segment DOIs
 * @SortBy = 4  => Order result set by total number of DOIs
 *------------------------------------------------------------------*/
BEGIN

SET NOCOUNT ON

DECLARE @DOIIdentifierID int
SELECT @DOIIdentifierID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'DOI'

-- Get the institutions and DOI prefix for each title DOI
SELECT	inst.InstitutionCode, inst.InstitutionName, inst.BHLMemberLibrary,
		SUBSTRING(ti.IdentifierValue, 1, 
						CASE WHEN CHARINDEX('/', ti.IdentifierValue) > 0 
							THEN CHARINDEX('/', ti.IdentifierValue) - 1 
							ELSE LEN(ti.IdentifierValue) 
						END) AS Prefix
INTO	#Title
FROM	dbo.Title t 
		INNER JOIN dbo.ItemTitle it ON t.TitleID = it.TitleID AND it.IsPrimary = 1
		INNER JOIN dbo.Item i ON it.ItemID= i.ItemID
		INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
		INNER JOIN dbo.InstitutionRole r 
			ON ii.InstitutionRoleID = r.InstitutionRoleID
			AND r.InstitutionRoleName = 'Holding Institution'
		INNER JOIN dbo.Institution inst ON ii.InstitutionCode = inst.InstitutionCode
		INNER JOIN dbo.Title_Identifier ti ON t.TitleID = ti.TitleID AND ti.IdentifierID = @DOIIdentifierID

-- Get the title stats
;WITH CTE AS (
	SELECT	InstitutionCode, InstitutionName, BHLMemberLibrary,
			CASE WHEN Prefix IN (SELECT Prefix FROM dbo.DOIPrefix WHERE AllowNew = 1) THEN 1 ELSE 0 END AS Minted,
			CASE WHEN Prefix IN (SELECT Prefix FROM dbo.DOIPrefix WHERE AllowNew = 0) THEN 1 ELSE 0 END AS Acquired,
			CASE WHEN Prefix NOT IN (SELECT Prefix FROM dbo.DOIPrefix) AND NOT @BHLOnly = 1 THEN 1 ELSE 0 END AS NonBHL		
	FROM	#Title
)
SELECT	InstitutionCode, InstitutionName, BHLMemberLibrary,
		SUM(Minted) AS Minted,
		SUM(Acquired) AS Acquired,
		SUM(NonBHL) AS NonBHL,
		SUM(Minted + Acquired + NonBHL) AS Total
INTO	#TitleStats
FROM	CTE
GROUP BY InstitutionCode, InstitutionName, BHLMemberLibrary

-- Get the institutions and DOI prefix for each segment DOI
SELECT	inst.InstitutionCode, inst.InstitutionName, inst.BHLMemberLibrary,
		SUBSTRING(ii.IdentifierValue, 1, 
						CASE WHEN CHARINDEX('/', ii.IdentifierValue) > 0 
							THEN CHARINDEX('/', ii.IdentifierValue) - 1 
							ELSE LEN(ii.IdentifierValue) 
						END) AS Prefix
INTO	#Segment
FROM	dbo.Segment s 
		INNER JOIN dbo.ItemInstitution iinst ON s.ItemID = iinst.ItemID
		INNER JOIN dbo.InstitutionRole r 
			ON iinst.InstitutionRoleID = r.InstitutionRoleID
			AND r.InstitutionRoleName = 'Contributor'
		INNER JOIN dbo.Institution inst ON iinst.InstitutionCode = inst.InstitutionCode
		INNER JOIN dbo.ItemIdentifier ii ON s.ItemID = ii.ItemID AND ii.IdentifierID = @DOIIdentifierID

-- Get the segment stats
;WITH CTE AS (
	SELECT	InstitutionCode, InstitutionName, BHLMemberLibrary,
			CASE WHEN Prefix IN (SELECT Prefix FROM dbo.DOIPrefix WHERE AllowNew = 1) THEN 1 ELSE 0 END AS Minted,
			CASE WHEN Prefix IN (SELECT Prefix FROM dbo.DOIPrefix WHERE AllowNew = 0) THEN 1 ELSE 0 END AS Acquired,
			CASE WHEN Prefix NOT IN (SELECT Prefix FROM dbo.DOIPrefix) AND NOT @BHLOnly = 1 THEN 1 ELSE 0 END AS NonBHL		
	FROM	#Segment
)
SELECT	InstitutionCode, InstitutionName, BHLMemberLibrary,
		SUM(Minted) AS Minted,
		SUM(Acquired) AS Acquired,
		SUM(NonBHL) AS NonBHL,
		SUM(Minted + Acquired + NonBHL) AS Total
INTO	#SegmentStats
FROM	CTE
GROUP BY InstitutionCode, InstitutionName, BHLMemberLibrary

-- Compile final result set
SELECT	ISNULL(x.InstitutionName, y.InstitutionName) AS InstitutionName, 
		ISNULL(x.BHLMemberLibrary, y.BHLMemberLibrary) AS BHLMemberLibrary, 
		ISNULL(x.Minted, 0) AS TitleMinted, 
		ISNULL(x.Acquired, 0) AS TitleAcquired, 
		ISNULL(x.NonBHL, 0) AS TitleNonBHL, 
		ISNULL(x.Total, 0) AS TitleTotalDOIs, 
		ISNULL(y.Minted, 0) AS SegmentMinted, 
		ISNULL(y.Acquired, 0) AS SegmentAcquired, 
		ISNULL(y.NonBHL, 0) AS SegmentNonBHL, 
		ISNULL(y.Total, 0) AS SegmentTotalDOIs, 
		ISNULL(x.Total, 0) + ISNULL(y.Total, 0) as TotalDOIs
INTO	#Final
FROM	#TitleStats x
		FULL OUTER JOIN #SegmentStats y on x.InstitutionCode = y.InstitutionCode
WHERE	ISNULL(x.Total, 0) > 0
OR		ISNULL(y.Total, 0) > 0

-- Return sorted results
IF @SortBy = 1
	SELECT * FROM #Final ORDER BY InstitutionName
IF @SortBy = 2
	SELECT * FROM #Final ORDER BY TitleTotalDOIs DESC, InstitutionName
IF @SortBy = 3
	SELECT * FROM #Final ORDER BY SegmentTotalDOIs DESC, InstitutionName
IF @SortBy = 4
	SELECT * FROM #Final ORDER BY TotalDOIs DESC, InstitutionName

END

GO
