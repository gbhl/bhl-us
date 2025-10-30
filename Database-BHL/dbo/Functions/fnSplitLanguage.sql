CREATE FUNCTION [dbo].[fnSplitLanguage](@MarcId int)
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
;WITH CTE AS (
	-- Return a CTE with each language and its associated source code (i.e. eng, iso639-3)
	SELECT	l.MarcDataFieldID, l.SubFieldValue AS Language, s.SubFieldValue AS Source
	FROM	(	SELECT	MarcDataFieldID, Code, SubFieldValue
				FROM	vwMarcDataField
				WHERE	DataFieldTag = '041'
				AND		Code IN ('a', 'b')
				AND		MarcID = @MarcId
			) l
			LEFT JOIN (
				SELECT	MarcDataFieldID, Code, SubFieldValue
				FROM	vwMarcDataField
				WHERE	DataFieldTag = '041'
				AND		Code = '2'
				AND		MarcID = @MarcId
				) s ON l.MarcDataFieldID = s.MarcDataFieldID
	)
-- Only include languages with no source code or a source code of iso639-2b
SELECT	@langs = @langs + COALESCE(Language, '')
FROM	CTE
WHERE	ISNULL(Source, 'iso639-2b') = 'iso639-2b'

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
