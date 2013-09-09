CREATE PROCEDURE [dbo].[CollectionSelectByCollectionName]

@CollectionName NVARCHAR(200)

AS 

SET NOCOUNT ON

SELECT 

	[CollectionID],
	[CollectionName],
	[CreationDate]

FROM [dbo].[Collection]

WHERE
	[CollectionName] = @CollectionName

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure CollectionSelectByCollectionName. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
