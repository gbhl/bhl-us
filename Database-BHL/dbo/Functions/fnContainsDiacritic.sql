
CREATE FUNCTION [dbo].[fnContainsDiacritic] 
(
	-- Add the parameters for the function here
	@Title NVARCHAR(2000)
)
RETURNS int
AS
/*
	Test with:
	SELECT	CHAR(dbo.fnContainsDiacritic(FullTitle)) as DiacriticCharacter, 
			TitleID, 
			FullTitle
	FROM	Title
	WHERE	dbo.fnContainsDiacritic(FullTitle) > 0
*/
BEGIN
	-- Declare the return variable here
	DECLARE @Result int

	DECLARE @TitleLen int
	DECLARE @Count int
	DECLARE	@AsciiValue int
	SET @TitleLen = LEN(@Title)
	SET @Count = 1
	SET @Result = 0

	IF (@TitleLen > 0)
	BEGIN
		-- Add the T-SQL statements to compute the return value here
		WHILE (@Count <= @TitleLen AND @Result = 0)
		BEGIN
			SET @AsciiValue = ASCII(SUBSTRING(@Title, @Count, 1))
			IF @AsciiValue BETWEEN 192 AND 255 SET @Result = @AsciiValue
			SET @Count = @Count + 1
		END
	END

	-- Return the result of the function
	RETURN @Result
END
