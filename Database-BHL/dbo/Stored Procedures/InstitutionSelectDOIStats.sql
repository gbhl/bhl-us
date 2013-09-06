
CREATE PROCEDURE [dbo].[InstitutionSelectDOIStats]

@SortBy int = 1

AS
/*-----------------------------------------------------
 * Returns a list of institutions, including the number
 * of titles with assigned DOIs that each institution
 * has contributed to. 
 *
 * @SortBy = 1  => Order result set by Institution Name
 * @SortBy = 2  => Order result set by number of DOIs
 *-----------------------------------------------------*/
BEGIN

SET NOCOUNT ON

SELECT	inst.InstitutionName, inst.BHLMemberLibrary, COUNT(*) AS NumberDOIs
INTO	#tmpDOIStats
FROM	dbo.Title t INNER JOIN dbo.Item i
			ON t.TitleID = i.PrimaryTitleID
		INNER JOIN dbo.Institution inst
			ON i.InstitutionCode = inst.InstitutionCode
		INNER JOIN dbo.DOI d
			ON t.TitleID = d.EntityID
			AND d.DOIEntityTypeID = 10 -- Title
GROUP BY
		inst.InstitutionName, inst.BHLMemberLibrary
		
IF @SortBy = 1
	SELECT * FROM #tmpDOIStats ORDER BY InstitutionName
ELSE
	SELECT * FROM #tmpDOIStats ORDER BY NumberDOIs DESC

END

