CREATE PROCEDURE [dbo].[CollectionSelectByUrl]

@CollectionURL nvarchar(50)

AS

BEGIN

SELECT	CollectionID,
		CollectionName
FROM	dbo.[Collection]
WHERE	CollectionURL = @CollectionURL
AND		LTRIM(RTRIM(@CollectionURL)) <> ''

END



