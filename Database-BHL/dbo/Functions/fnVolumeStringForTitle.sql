SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fnVolumeStringForTitle] 
(
	@TitleID int
)
RETURNS nvarchar(1024)
AS 

BEGIN
	
	DECLARE @VolumeString nvarchar(1024)

	DECLARE @CurrentRecord int
	SELECT @CurrentRecord = 1

	SELECT	@VolumeString = COALESCE(@VolumeString, '') +
						(CASE WHEN @CurrentRecord = 1 THEN '' ELSE '|' END) +  
						b.Volume,
			@CurrentRecord = @CurrentRecord + 1
	FROM	dbo.Title t 
			INNER JOIN dbo.ItemTitle ti ON t.TitleID = ti.TitleID
			INNER JOIN dbo.Item i ON ti.ItemID = i.ItemID
			INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
	WHERE	t.TitleID = @TitleID
	AND		i.ItemStatusID = 40
	ORDER BY
			ti.ItemSequence

	RETURN LTRIM(RTRIM(COALESCE(@VolumeString, '')))
END


GO
