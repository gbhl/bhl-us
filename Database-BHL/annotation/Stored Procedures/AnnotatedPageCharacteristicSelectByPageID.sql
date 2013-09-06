-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [annotation].[AnnotatedPageCharacteristicSelectByPageID]

@PageID INT

AS 

SET NOCOUNT ON

SELECT 

	apc.[AnnotatedPageCharacteristicID],
	apc.[AnnotatedPageID],
	apc.[CharacteristicDetail],
	apc.[CharacteristicDetailClean],
	apc.[CreationDate],
	apc.[LastModifiedDate]

FROM [annotation].[AnnotatedPageCharacteristic]  apc
JOIN [annotation].[AnnotatedPage] ap
ON ap.AnnotatedPageID = apc.AnnotatedPageID
AND ap.PageID = @PageID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotatedPageCharacteristicSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
