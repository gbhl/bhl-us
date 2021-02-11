CREATE FUNCTION [dbo].[fnNoteIndexStringForTitle] 
(
	@TitleID int,
	@Prefix nvarchar(10) = ''
)
RETURNS nvarchar(max)
AS 

BEGIN
	
	DECLARE @NoteString nvarchar(max)

	DECLARE @CurrentRecord int
	SELECT @CurrentRecord = 1

	/*
	SELECT 
		@NoteString = COALESCE(@NoteString, '') +
					(CASE WHEN @CurrentRecord = 1 THEN '' ELSE '|||' END) + LTRIM(RTRIM(NoteText)),
		@CurrentRecord = @CurrentRecord + 1
	FROM	dbo.TitleNote n
			INNER JOIN dbo.NoteType nt ON n.NoteTypeID = nt.NoteTypeID
	WHERE	TitleID = @TitleID
	AND		nt.MarcDataFieldTag IN ('505', '520', '545')
	ORDER BY
			NoteSequence
	*/

	SELECT	@NoteString = COALESCE(@NoteString, '') +
						(CASE WHEN @CurrentRecord = 1 THEN '' ELSE '|||' END) + LTRIM(RTRIM(NoteText)),
			@CurrentRecord = @CurrentRecord + 1
	FROM	(
			-- Title Notes
			SELECT	nt.NoteTypeName, n.NoteText, n.NoteSequence
			FROM	dbo.TitleNote n
					INNER JOIN dbo.NoteType nt ON n.NoteTypeID = nt.NoteTypeID
			WHERE	nt.MarcDataFieldTag IN ('505', '520', '545')
			AND		TitleID = @TitleID
			UNION
			-- Flickr tags from BHLImport database
			SELECT DISTINCT 
					CASE WHEN mtNamespace IN ('geo', 'geography') THEN mtPredicate ELSE mtNameSpace END AS NoteTypeName,
					CASE WHEN mtNamespace IN ('geo', 'geography') THEN mtPredicate ELSE mtNameSpace END + ': ' + mtValue  + ' (source: Flickr Tags)' AS NoteText, 
					10000 AS NoteSequence
			FROM	BHLImportPageFlickrMachineTag 
			WHERE	(
					(mtNamespace = 'taxonomy' AND mtPredicate = 'common')
			OR		(mtNamespace IN ('geo', 'geography') AND mtPredicate IN ('lat', 'lon', 'continent', 'country', 'state', 'city', 'county', 'island', 'lake', 'province', 'region', 'locality'))
			OR		(mtNamespace IN ('artist', 'engraver', 'colorist', 'author', 'illustrator', 'lithographer', 'people', 'person', 'photographer') AND mtPredicate = 'name')
					)
			AND		TitleID = @TitleID
			) x
	ORDER BY NoteSequence, NoteTypeName, NoteText
	
	SELECT @NoteString = LTRIM(RTRIM(COALESCE(@NoteString, '')))
	IF (@NoteString <> '') SELECT @NoteString = @Prefix + @NoteString

	RETURN @NoteString
END

