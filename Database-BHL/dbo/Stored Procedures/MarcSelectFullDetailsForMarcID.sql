CREATE PROCEDURE [dbo].[MarcSelectFullDetailsForMarcID]

@MarcID INT

AS
BEGIN

SET NOCOUNT ON

SELECT	TOP 1 '_Leader' AS DataFieldTag, '' AS Indicator1, '' AS Indicator2, '' AS Code, Leader AS [SubFieldValue]
FROM	vwMarcControl
WHERE	MarcID = @MarcID
UNION
SELECT	Tag AS DataFieldTag, '' AS Indicator1, '' AS Indicator2, '' AS Code, [Value] AS SubFieldValue
FROM	vwMarcControl
WHERE	MarcID = @MarcID
UNION
SELECT	DataFieldTag, Indicator1, Indicator2, Code, SubFieldValue
FROM	vwMarcDataField 
WHERE	MarcID = @MarcID
ORDER BY
		DataFieldTag,
		Code,
		SubFieldValue

END
