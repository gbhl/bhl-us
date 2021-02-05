CREATE PROCEDURE dbo.DOIEntityTypeSelectAll

AS

BEGIN

SET NOCOUNT ON

SELECT	DOIEntityTypeID,
		DOIEntityTypeName
FROM	dbo.DOIEntityType
ORDER BY DOIEntityTypeName

END

GO
