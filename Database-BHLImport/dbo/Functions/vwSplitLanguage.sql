CREATE FUNCTION [dbo].[vwSplitLanguage](@ItemId int)
	RETURNS @languages TABLE (LanguageCode nvarchar(10))
AS
BEGIN
	
DECLARE @langs varchar(50)
SET @langs = ''

-- Get a single string listing all languages for the item
-- i.e. EngGerLatFre
-- This might be the way that languages are stored in the 
-- MARC 41 record, so we just concatenate all MARC 41
-- records for an item to get the full list of languages.
SELECT	@langs = @langs + COALESCE(SubFieldValue, '')
FROM	vwIAMarcDataField
WHERE	DataFieldTag = '041'
AND		Code IN ('a', 'b')
AND		ItemID = @ItemId

-- Parse each individual language code into the return table.
DECLARE	@pos smallint
SET @pos = 1
WHILE @pos < LEN(@langs) 
BEGIN 
	INSERT	@languages (LanguageCode)
	VALUES	(SUBSTRING(@langs, @pos, 3))

	SET @pos = @pos + 3
END 

RETURN 

END
