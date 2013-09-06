-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [annotation].[AnnotatedItemCheckForSurrogate]
	--Returns 1 if belongs to Darwin's Library, 0 if Surrogate
	@ItemID int
AS
BEGIN
	SET NOCOUNT ON;

    SELECT      CAST (CASE WHEN COUNT(*) > 0 THEN 1
											 ELSE 0
					  END AS bit) AS IsReal
FROM  annotation.AnnotatedItem ai INNER JOIN dbo.Item i

                  ON ai.ItemID = i.ItemID

                  AND i.InstitutionCode = 'CUL' -- Cambridge University Libraries

            INNER JOIN dbo.ItemCollection ic

                  ON i.ItemID = ic.ItemID

                  AND ic.CollectionID = 4 -- Darwin's Library

WHERE i.ItemID = @ItemID
END
