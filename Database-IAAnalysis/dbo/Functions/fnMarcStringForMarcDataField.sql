CREATE FUNCTION [dbo].[fnMarcStringForMarcDataField] 
(
	@MarcDataFieldID int
)
RETURNS nvarchar(1024)
AS 
BEGIN
	DECLARE @SubjectString nvarchar(1024)

	SELECT	TOP 1 @SubjectString = DataFieldTag + ' ' + 
							CASE WHEN Indicator1 = '' THEN ' ' ELSE Indicator1 END +
							CASE WHEN Indicator2 = '' THEN ' ' ELSE Indicator2 END
	FROM	vwMarcDetail
	WHERE	MarcDataFieldID = @MarcDataFieldID

	SELECT @SubjectString = COALESCE(@SubjectString, '') + '|' + m.Code + m.SubFieldValue
	FROM vwMarcDetail m
	WHERE m.MarcDataFieldID = @MarcDataFieldID
	ORDER BY m.MarcSubFieldID ASC

	RETURN LTRIM(RTRIM(COALESCE(@SubjectString, '')))
END
