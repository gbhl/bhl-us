CREATE PROCEDURE dbo.InstitutionGroupSelectAll

AS 

SET NOCOUNT ON

SELECT	ig.InstitutionGroupID,
		InstitutionGroupName,
		InstitutionGroupDescription,
		SUM(CASE WHEN igi.InstitutionGroupInstitutionID IS NULL THEN 0 ELSE 1 END) AS NumberOfInstitutions
FROM	dbo.InstitutionGroup ig
		LEFT JOIN dbo.InstitutionGroupInstitution igi ON ig.InstitutionGroupID = igi.InstitutionGroupID
GROUP BY
		ig.InstitutionGroupID,
		InstitutionGroupName,
		InstitutionGroupDescription
ORDER BY
		InstitutionGroupName

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure InstitutionGroupSelectAll. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
