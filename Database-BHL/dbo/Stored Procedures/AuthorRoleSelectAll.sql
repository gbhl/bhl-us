CREATE PROCEDURE [dbo].[AuthorRoleSelectAll]
AS 

SET NOCOUNT ON

SELECT	AuthorRoleID,
		RoleDescription,
		MARCDataFieldTag
FROM	dbo.AuthorRole
ORDER BY RoleDescription


