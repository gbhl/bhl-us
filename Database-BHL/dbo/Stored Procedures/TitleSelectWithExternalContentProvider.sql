CREATE PROCEDURE dbo.TitleSelectWithExternalContentProvider

AS

BEGIN

DECLARE @ExtContentHolderID int

SELECT @ExtContentHolderID = InstitutionRoleID FROM dbo.InstitutionRole WHERE InstitutionRoleName = 'External Content Holder'

SELECT	t.TitleID, t.FullTitle, ti.TitleInstitutionID, i.InstitutionName, ti.Url
FROM	dbo.Title t
		INNER JOIN dbo.TitleInstitution ti ON t.TitleID = ti.TitleID
		INNER JOIN dbo.Institution i on ti.InstitutionCode = i.InstitutionCode
WHERE	t.PublishReady = 1
AND		ti.InstitutionRoleID = @ExtContentHolderID
ORDER BY t.TitleID

END
