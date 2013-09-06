CREATE PROCEDURE [dbo].[TitleCollectionSelectByTitle]

@TitleID INT 

AS 

SET NOCOUNT ON

SELECT 	TC.TitleCollectionID,
		TC.[TitleID],
		TC.[CollectionID],
		C.CollectionName,
		C.CollectionDescription
FROM	[dbo].[TitleCollection] TC INNER JOIN dbo.[Collection] C 
			ON TC.CollectionID = C.CollectionID
WHERE	TC.[TitleID] = @TitleID 
ORDER BY C.CollectionName

