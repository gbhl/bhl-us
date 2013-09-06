CREATE PROCEDURE dbo.TitleVariantTypeSelectAll

AS

BEGIN

SET NOCOUNT ON

SELECT	TitleVariantTypeID,
		TitleVariantTypeName,
		MARCTag,
		MARCIndicator2,
		TitleVariantLabel
FROM	dbo.TitleVariantType
ORDER BY
		TitleVariantTypeName	
		
END

