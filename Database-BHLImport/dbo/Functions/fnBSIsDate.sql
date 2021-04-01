CREATE FUNCTION dbo.fnBSIsDate
(
	@Date NVARCHAR(20)
)
RETURNS INT
AS
BEGIN
	-- Validates dates that are received from the BioStor Harvest process.

	-- Accepted date formats are:
	--		YYYY
	--		YYYY-MM
	--		YYYY-MM-DD
	--		YYYY/MM/DD

	RETURN CASE 
			WHEN @Date LIKE '[1-9][0-9][0-9][0-9]' THEN 1
			WHEN @Date LIKE '[1-9][0-9][0-9][0-9][-][0][1-9]' THEN 1
			WHEN @Date LIKE '____[-/]__[-/]__' THEN ISDATE(@Date)
			ELSE 0
			END
END

GO
