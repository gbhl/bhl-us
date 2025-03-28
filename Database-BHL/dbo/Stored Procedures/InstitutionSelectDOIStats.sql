SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InstitutionSelectDOIStats]

@SortBy int = 1,
@BHLOnly int = 1

AS
/*-----------------------------------------------------
 * Returns a list of institutions, including the number
 * of titles and segments with assigned DOIs that each 
 * institution has contributed to. 
 *
 * @SortBy = 1  => Order result set by Institution Name
 * @SortBy = 2  => Order result set by number of title DOIs
 * @SortBy = 3  => Order result set by number of segment DOIs
 * @SortBy = 4  => Order result set by number of total DOIs
 *-----------------------------------------------------*/
BEGIN

SET NOCOUNT ON

DECLARE @DOIIdentifierID int
SELECT @DOIIdentifierID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'DOI'

SELECT	inst.InstitutionCode, inst.InstitutionName, inst.BHLMemberLibrary, COUNT(*) AS TitleDOIs
INTO	#tmpTitle
FROM	dbo.Title t 
		INNER JOIN dbo.ItemTitle it ON t.TitleID = it.TitleID AND it.IsPrimary = 1
		INNER JOIN dbo.Item i ON it.ItemID= i.ItemID
		INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
		INNER JOIN dbo.InstitutionRole r 
			ON ii.InstitutionRoleID = r.InstitutionRoleID
			AND r.InstitutionRoleName = 'Holding Institution'
		INNER JOIN dbo.Institution inst ON ii.InstitutionCode = inst.InstitutionCode
		INNER JOIN dbo.Title_Identifier ti ON t.TitleID = ti.TitleID AND ti.IdentifierID = @DOIIdentifierID
WHERE	ti.IdentifierValue LIKE '%10.5962%' 
OR		@BHLOnly = 0
GROUP BY
		inst.InstitutionCode, inst.InstitutionName, inst.BHLMemberLibrary

SELECT	inst.InstitutionCode, inst.InstitutionName, inst.BHLMemberLibrary, COUNT(*) AS SegmentDOIs
INTO	#tmpSegment
FROM	dbo.Segment s 
		INNER JOIN dbo.ItemInstitution iinst ON s.ItemID = iinst.ItemID
		INNER JOIN dbo.InstitutionRole r 
			ON iinst.InstitutionRoleID = r.InstitutionRoleID
			AND r.InstitutionRoleName = 'Contributor'
		INNER JOIN dbo.Institution inst ON iinst.InstitutionCode = inst.InstitutionCode
		INNER JOIN dbo.ItemIdentifier ii ON s.ItemID = ii.ItemID AND ii.IdentifierID = @DOIIdentifierID
WHERE	ii.IdentifierValue LIKE '%10.5962%' 
OR		@BHLOnly = 0
GROUP BY
		inst.InstitutionCode, inst.InstitutionName, inst.BHLMemberLibrary

SELECT	i.InstitutionCode, i.InstitutionName, i.BHLMemberLibrary, 0 AS TitleDOIs, 0 AS SegmentDOIs
INTO	#tmpFinal
FROM	dbo.Institution i

UPDATE	#tmpFinal
SET		TitleDOIs = t.TitleDOIs
FROM	#tmpFinal f INNER JOIN #tmpTitle t ON f.InstitutionCode = t.InstitutionCode

UPDATE	#tmpFinal
SET		SegmentDOIs = s.SegmentDOIs
FROM	#tmpFinal f INNER JOIN #tmpSegment s ON f.InstitutionCode = s.InstitutionCode

SELECT	InstitutionName, BHLMemberLibrary, TitleDOIs, SegmentDOIs, TitleDOIs + SegmentDOIs AS TotalDOIs
INTO	#tmpSort
FROM	#tmpFinal
WHERE	TitleDOIs > 0
OR		SegmentDOIs > 0

IF @SortBy = 1
	SELECT * FROM #tmpSort ORDER BY InstitutionName
IF @SortBy = 2
	SELECT * FROM #tmpSort ORDER BY TitleDOIs DESC, InstitutionName
IF @SortBy = 3
	SELECT * FROM #tmpSort ORDER BY SegmentDOIs DESC, InstitutionName
IF @SortBy = 4
	SELECT * FROM #tmpSort ORDER BY TotalDOIs DESC, InstitutionName

END

GO
