CREATE FUNCTION [dbo].[fnNoteStringForTitle] 
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

	SELECT 
		@NoteString = COALESCE(@NoteString, '') +
					(CASE WHEN @CurrentRecord = 1 THEN '' ELSE ' --- ' END) + LTRIM(RTRIM(NoteText)),
		@CurrentRecord = @CurrentRecord + 1
	FROM	dbo.TitleNote
	WHERE	TitleID = @TitleID
	ORDER BY
			NoteSequence
	
	SELECT @NoteString = LTRIM(RTRIM(COALESCE(@NoteString, '')))
	IF (@NoteString <> '') SELECT @NoteString = @Prefix + @NoteString

	RETURN @NoteString
END
