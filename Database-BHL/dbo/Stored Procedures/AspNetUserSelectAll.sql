CREATE PROCEDURE dbo.AspNetUserSelectAll

AS

BEGIN

SET NOCOUNT ON 

DECLARE @columns NVARCHAR(MAX)
DECLARE @sql NVARCHAR(MAX)

SET @columns = N''

SELECT	@columns += N', p.' + QUOTENAME(RoleName)
FROM	(	SELECT DISTINCT r.Name AS RoleName, r.DisplaySequence 
			FROM dbo.AspNetUserRoles ur INNER JOIN dbo.AspNetRoles r ON ur.RoleId = r.Id) AS x
ORDER BY DisplaySequence DESC


SET @sql = N'
SELECT FirstName, LastName, UserName, Email, CASE WHEN Disabled = 1 THEN ''X'' ELSE '''' END AS Disabled, 
	LockoutEndDateUtc, LastLoginDateUtc, InstitutionName, InstitutionGroup, ' + STUFF(@columns, 1, 2, '') + '
FROM
(
  SELECT u.Id, u.FirstName, u.LastName, u.UserName, u.Email, u.Disabled, u.LockoutEndDateUtc, u.LastLoginDateUtc,
	i.InstitutionName, dbo.fnGroupStringForInstitution(i.InstitutionCode) AS InstitutionGroup,
	r.Name, ''X'' AS [Rows]
  FROM dbo.AspNetUsers u
	LEFT JOIN dbo.AspNetUserRoles ur ON u.id = ur.UserId
	LEFT JOIN dbo.AspNetRoles r ON ur.RoleId = r.Id
	LEFT JOIN dbo.Institution i ON u.HomeInstitutionCode = i.InstitutionCode
) AS j
PIVOT
(
  MAX([Rows]) FOR Name IN (' + STUFF(REPLACE(@columns, ', p.[', ',['), 1, 1, '') + ')
) AS p
ORDER BY LastName, FirstName, Id'

EXEC sp_executesql @sql

END
