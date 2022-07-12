CREATE PROCEDURE dbo.DOIEntityTypeSelectWithDoi

AS

BEGIN


SELECT	t.DOIEntityTypeID, DOIEntityTypeName
FROM	dbo.DOIEntityType t
		INNER JOIN dbo.DOI d ON t.DOIEntityTypeID = d.DOIEntityTypeID
GROUP BY t.DOIEntityTypeID, DOIEntityTypeName
ORDER BY DOIEntityTypeName

END

GO
