CREATE FUNCTION [dbo].[fnReverseAuthorNameISV] (@FullName nvarchar(300))
RETURNS TABLE WITH SCHEMABINDING 
AS
	-- For performance reasons, this function is implemented as an "inline scalar function" rather than a "real" scalar function
	RETURN	WITH FullName (Name) AS  
			(  
				-- Trim off one trailing comma from original string
				SELECT CASE WHEN RIGHT(@FullName, 1) = ',' THEN SUBSTRING(@FullName, 1, LEN(@FullName) - 1) ELSE @FullName END
			)  
			SELECT LTRIM(RTRIM(
						CASE 
						WHEN CHARINDEX(',', Name) = LEN(Name) THEN
							-- Comma at end of name, return original string
							Name
						WHEN CHARINDEX(',', Name) > 0 THEN
							-- Reversed string
							SUBSTRING(Name, CHARINDEX(',', Name) + 1, LEN(Name) - CHARINDEX(',', Name) + 1) + ' ' + SUBSTRING(Name, 1, CHARINDEX(',', Name) - 1)
						ELSE
							-- No comma, return original string
							@FullName
						END
						)) AS FullNameReversed
			FROM FullName

GO
