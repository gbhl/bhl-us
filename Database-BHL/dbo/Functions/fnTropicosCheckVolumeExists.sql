CREATE FUNCTION [dbo].[fnTropicosCheckVolumeExists]
(
	@TitleID int, 
	@Volume nvarchar(20)
)
RETURNS INT
AS
BEGIN

--always check if volume exists even when volume is null/empty
--books will not have a volume
IF(@Volume IS NULL OR @Volume = '')
BEGIN
	IF EXISTS
	(	
		SELECT TOP 1 p.ItemID
		FROM Page p WITH (NOLOCK)
		INNER JOIN Item i WITH (NOLOCK) ON p.ItemID = i.ItemID
		INNER JOIN Title t WITH (NOLOCK) ON i.PrimaryTitleID = t.TitleID
		WHERE t.TitleID = @TitleID AND
			t.PublishReady = 1 AND
			i.ItemStatusID = 40 AND
			(p.Volume IS NULL OR p.Volume = '' or p.Volume='1')
	)
	BEGIN
		RETURN 1
	END
END

IF EXISTS
(	
	SELECT TOP 1 p.ItemID
	FROM Page p
	INNER JOIN Item i WITH (NOLOCK) ON p.ItemID = i.ItemID
	INNER JOIN Title t WITH (NOLOCK) ON i.PrimaryTitleID = t.TitleID
	WHERE t.TitleID = @TitleID AND
		t.PublishReady = 1 AND
		i.ItemStatusID = 40 AND
		p.Volume = @Volume
)
BEGIN
	RETURN 1
END

IF EXISTS
(	
	SELECT TOP 1 i.ItemID
	FROM Item i JOIN Title t ON i.PrimaryTitleID = t.TitleID
	WHERE t.TitleID = @TitleID AND
		t.PublishReady = 1 AND
		i.ItemStatusID = 40 AND
		i.Volume = @Volume
)
BEGIN
	RETURN 1
END

IF EXISTS
(	
	SELECT TOP 1 i.ItemID
	FROM Item i JOIN Title t ON i.PrimaryTitleID = t.TitleID
	WHERE t.TitleID = @TitleID AND
		t.PublishReady = 1 AND
		i.ItemStatusID = 40 AND
		(
			i.Volume LIKE @Volume + '%' OR
			i.Volume LIKE '%[^0-9]' + @Volume + '%'
		)
)
BEGIN
	RETURN 1
END

RETURN 0

END
