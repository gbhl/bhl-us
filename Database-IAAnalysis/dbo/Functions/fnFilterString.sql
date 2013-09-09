CREATE FUNCTION [dbo].[fnFilterString]
(
	@Source VARCHAR(8000),
	@Filter VARCHAR(8000),
	@Replacement CHAR(1)
)
RETURNS VARCHAR(8000)
AS
BEGIN
	/*
	@Source is the string to be filtered
	@Filter is a regular expression identifying the 'valid' characters in the returned string
	@Replacement identifies the character to use to replace 'invalid' characters
	*/
	DECLARE	@Index SMALLINT

	SET	@Index = DATALENGTH(@Source)

	WHILE @Index > 0
		IF SUBSTRING(@Source, @Index, 1) LIKE @Filter
			SET	@Index = @Index - 1
		ELSE
			SELECT	@Source = STUFF(@Source, @Index, 1, @Replacement),
				@Index =  @Index - 1

	RETURN REPLACE(@Source, ' ', '')
END
