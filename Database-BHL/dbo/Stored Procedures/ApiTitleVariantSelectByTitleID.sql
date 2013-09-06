CREATE PROCEDURE [dbo].[ApiTitleVariantSelectByTitleID]

@TitleID int

AS

BEGIN

SET NOCOUNT ON

SELECT	vt.TitleVariantTypeName,
		v.Title + ISNULL(' ' + v.TitleRemainder, '') + 
				ISNULL(' ' + v.PartNumber, '') + 
				ISNULL(' ' + v.PartName, '') AS Title
FROM	dbo.TitleVariant v INNER JOIN dbo.TitleVariantType vt
			ON v.TitleVariantTypeID = vt.TitleVariantTypeID
		INNER JOIN dbo.Title t
			ON v.TitleID = t.TitleID
WHERE	t.PublishReady = 1
AND		t.TitleID = @TitleID
ORDER BY
		vt.TitleVariantTypeName,
		v.Title,
		v.TitleRemainder,
		v.PartNumber,
		v.PartName

END

