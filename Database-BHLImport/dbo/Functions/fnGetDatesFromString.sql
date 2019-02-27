CREATE FUNCTION dbo.fnGetDatesFromString
(
	@DateString nvarchar(max)
)
RETURNS @dates TABLE (
        StartDate VARCHAR(100),
        EndDate VARCHAR(100)
    )
AS 

BEGIN
	DECLARE @StartDate nvarchar(100)
	DECLARE @EndDate nvarchar(100)

	SELECT	@DateString = 
			RTRIM(LTRIM(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
			REPLACE(
				@DateString, 
			' [from old catalog]', ''),
			',', ''),
			'(', ''),
			')', ''),
			'[', ''),
			']', ''),
			':', '')
			))

	SELECT	@StartDate = CASE 
			WHEN CHARINDEX('-', @DateString) > 0 THEN SUBSTRING(@DateString, 1, CHARINDEX('-', @DateString) - 1)
			ELSE @DateString 
			END,
			@EndDate = CASE WHEN CHARINDEX('-', @DateString) > 0 THEN SUBSTRING(@DateString, CHARINDEX('-', @DateString) + 1, LEN(@DateString))
			ELSE ''
			END

	INSERT INTO	@dates
	SELECT	LTRIM(RTRIM(REPLACE(REPLACE(CASE WHEN LEFT(@StartDate, 2) = 'd.' THEN '' ELSE @StartDate END, 'b.', ''), '.', ' '))),
			LTRIM(RTRIM(REPLACE(REPLACE(CASE WHEN LEFT(@StartDate, 2) = 'd.' THEN @StartDate ELSE @EndDate END, 'd.', ''), '.', ' ')))

	RETURN
END
