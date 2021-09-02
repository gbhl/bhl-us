CREATE PROCEDURE dbo.InstitutionSelectForMonthlyStats

AS 

SET NOCOUNT ON

SELECT	ins.InstitutionCode
INTO	#tmpInstitution
FROM	dbo.Institution ins WITH (NOLOCK)
		INNER JOIN dbo.ItemInstitution ii WITH (NOLOCK) ON ins.InstitutionCode = ii.InstitutionCode
		INNER JOIN dbo.Item it WITH (NOLOCK) ON ii.ItemID = it.ItemID
		INNER JOIN dbo.Book b WITH (NOLOCK) ON it.ItemID = b.ItemID
WHERE	it.ItemStatusID = 40
UNION 
SELECT	ins.InstitutionCode
FROM	dbo.Institution ins WITH (NOLOCK)
		INNER JOIN dbo.ItemInstitution ii WITH (NOLOCK) ON ins.InstitutionCode = ii.InstitutionCode
		INNER JOIN dbo.Item it WITH (NOLOCK) ON ii.ItemID = it.ItemID
		INNER JOIN dbo.vwSegment s WITH (NOLOCK) ON it.ItemID = s.ItemID
		INNER JOIN dbo.Book b WITH (NOLOCK) ON s.BookID = b.BookID AND b.IsVirtual = 1
WHERE	it.ItemStatusID IN (30, 40)

SELECT	InstitutionCode,
		InstitutionName,
		Note,
		ISNULL(InstitutionUrl, '') AS InstitutionUrl,
		BHLMemberLibrary
FROM	dbo.Institution
WHERE	InstitutionCode IN (SELECT DISTINCT InstitutionCode FROM #tmpInstitution)
ORDER BY InstitutionName

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure InstitutionSelectForMonthlyStats. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
