CREATE PROCEDURE dbo.AspNetUserSelectWithImportFiles

AS

BEGIN

SET NOCOUNT ON

SELECT DISTINCT
		u.Id, u.FirstName, u.LastName, ISNULL(InstitutionName, '') AS InstitutionName
FROM	dbo.AspNetUsers u 
		INNER JOIN import.ImportFile f ON u.Id = f.CreationUserID
		LEFT JOIN dbo.Institution i ON u.HomeInstitutionCode = i.InstitutionCode
ORDER BY u.LastName, u.FirstName

END

GO
