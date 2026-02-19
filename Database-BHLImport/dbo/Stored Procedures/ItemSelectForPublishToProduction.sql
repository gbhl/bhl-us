CREATE PROCEDURE dbo.ItemSelectForPublishToProduction

@Barcode nvarchar(200) = ''

AS

BEGIN

SET NOCOUNT ON

SELECT	BarCode 
FROM	dbo.Item 
WHERE	ImportStatusID = 10 
AND		ImportSourceID = 1 
AND		(Barcode = @Barcode OR @Barcode = '')

END 

GO
