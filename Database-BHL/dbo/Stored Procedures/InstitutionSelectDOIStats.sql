
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

SELECT	inst.InstitutionCode, inst.InstitutionName, inst.BHLMemberLibrary, COUNT(*) AS TitleDOIs
INTO	#tmpTitle
FROM	dbo.Title t INNER JOIN dbo.Item i
			ON t.TitleID = i.PrimaryTitleID
		INNER JOIN dbo.Institution inst
			ON i.InstitutionCode = inst.InstitutionCode
		INNER JOIN dbo.DOI d
			ON t.TitleID = d.EntityID
			AND d.DOIEntityTypeID = 10 -- Title
			AND (d.DOIStatusID = 100 OR @BHLOnly = 0) -- BHL-assigned DOI
GROUP BY
		inst.InstitutionCode, inst.InstitutionName, inst.BHLMemberLibrary

SELECT	inst.InstitutionCode, inst.InstitutionName, inst.BHLMemberLibrary, COUNT(*) AS SegmentDOIs
INTO	#tmpSegment
FROM	dbo.Segment s INNER JOIN dbo.Institution inst
			ON s.ContributorCode = inst.InstitutionCode
		INNER JOIN dbo.DOI d
			ON s.SegmentID = d.EntityID
			AND d.DOIEntityTypeID = 40 -- Segment
			AND (d.DOIStatusID = 100 OR @BHLOnly = 0) -- BHL-assigned DOI
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

