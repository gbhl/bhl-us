CREATE PROCEDURE dbo.TitleVariantSelectByTitleID

@TitleID int

AS

BEGIN

SET NOCOUNT ON

SELECT	v.TitleVariantID,
		v.TitleID,
		v.TitleVariantTypeID,
		v.Title, 
		v.TitleRemainder, 
		v.PartNumber, 
		v.PartName, 
		vt.TitleVariantLabel,
		vt.TitleVariantTypeName
FROM	dbo.TitleVariant v INNER JOIN dbo.TitleVariantType vt
			ON v.TitleVariantTypeID = vt.TitleVariantTypeID
WHERE	TitleID = @TitleID
ORDER BY
		vt.TitleVariantLabel,
		v.Title,
		v.TitleRemainder,
		v.PartNumber,
		v.PartName

END

