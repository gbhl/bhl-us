CREATE PROCEDURE [dbo].[PageSummarySelectBarcodeForTitleID]

@TitleID int

AS 
BEGIN

SET NOCOUNT ON

SELECT DISTINCT BarCode FROM PageSummaryView WHERE TitleID = @TitleID
UNION
SELECT DISTINCT MarcBibID FROM PageSummaryView WHERE TitleID = @TitleID

END
