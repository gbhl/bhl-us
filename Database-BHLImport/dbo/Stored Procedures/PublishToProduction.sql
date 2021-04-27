
CREATE PROCEDURE [dbo].[PublishToProduction]

@ImportSourceID INT = NULL

AS

BEGIN

SET NOCOUNT ON

-- @ImportSourceID parameter is no longer necessary  - 2012/05/02 MWL

-- Import material from Internet Archive (one Internet Archive item at a time)
DECLARE curBarCodes CURSOR READ_ONLY
FOR	SELECT BarCode FROM dbo.Item 
	WHERE ImportStatusID = 10 AND ImportSourceID = 1

DECLARE @BarCode varchar(200)
OPEN curBarCodes

FETCH NEXT FROM curBarCodes INTO @BarCode
WHILE (@@fetch_status <> -1)
BEGIN
	IF (@@fetch_status <> -2)
	BEGIN
		exec dbo.ItemPublishToProductionIA @BarCode WITH RECOMPILE
	END
	FETCH NEXT FROM curBarCodes INTO @BarCode
END

CLOSE curBarCodes
DEALLOCATE curBarCodes

END


