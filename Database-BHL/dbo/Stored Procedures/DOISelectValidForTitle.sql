CREATE PROCEDURE dbo.DOISelectValidForTitle

@TitleID int

AS

BEGIN

SET NOCOUNT ON

SELECT	d.DOIID,
		d.DOIBatchID,
		d.DOIName
FROM	dbo.DOI d INNER JOIN dbo.DOIEntityType t
			ON d.DOIEntityTypeID = t.DOIEntityTypeID
			AND t.DOIEntityTypeName = 'Title'
WHERE	d.EntityID = @TitleID
AND		d.IsValid = 1

END

