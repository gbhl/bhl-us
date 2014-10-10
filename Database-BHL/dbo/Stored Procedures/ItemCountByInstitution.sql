CREATE PROCEDURE [dbo].[ItemCountByInstitution]
	@InstitutionCode nvarchar(10)
AS

BEGIN

SET NOCOUNT ON

SELECT	COUNT(*)
FROM	dbo.Item
WHERE	ItemStatusID = 40
AND		InstitutionCode = @InstitutionCode

END
