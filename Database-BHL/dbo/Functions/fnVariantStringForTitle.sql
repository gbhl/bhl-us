
CREATE FUNCTION [dbo].[fnVariantStringForTitle] 
(
	@TitleID int
)
RETURNS nvarchar(MAX)
AS 

BEGIN
	
	DECLARE @VariantString nvarchar(MAX)

	SELECT @VariantString  = STUFF((
		SELECT '|' + RTRIM(LTRIM(RTRIM(Title)) + ' ' + LTRIM(RTRIM(TitleRemainder)) + ' ' +
						LTRIM(RTRIM(PartNumber)) + ' ' + LTRIM(RTRIM(PartName)))
		FROM	dbo.TitleVariant tv
		WHERE	tv.TitleID = @TitleID
		ORDER BY
				Title, TitleRemainder, PartNumber, PartName
		FOR XML PATH('')
		),1,1,'')

	RETURN LTRIM(RTRIM(COALESCE(@VariantString, '')))
END


