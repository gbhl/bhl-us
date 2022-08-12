CREATE PROCEDURE dbo.AspNetUserSelectWithDoi

AS

BEGIN

SELECT	u.id, u.FirstName, u.LastName, ISNULL(InstitutionName, '') AS InstitutionName
FROM	dbo.AspNetUsers u
		INNER JOIN dbo.DOI d ON u.id = d.CreationUserID
		LEFT JOIN dbo.Institution i ON u.HomeInstitutionCode = i.InstitutionCode
GROUP BY u.id, u.FirstName, u.LastName, ISNULL(InstitutionName, '')
ORDER BY CASE WHEN u.id = 1 THEN 0 ELSE 1 END, u.LastName, u.FirstName

END

GO
