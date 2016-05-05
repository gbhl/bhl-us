-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CollectionSelectByNameLike]
@CollectionName varchar(50)
AS
SET NOCOUNT ON

SELECT DISTINCT
	c.[CollectionID],
	c.[CollectionName],
	c.[CollectionDescription],
	c.[CreationDate],
	c.[LastModifiedDate]
FROM [dbo].[Collection] c
WHERE	c.CollectionName LIKE @CollectionName + '%'
ORDER BY c.CollectionName

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure CollectionSelectByNameLike. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END