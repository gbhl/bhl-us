CREATE PROCEDURE [dbo].[AuthorTypeSelectAll]

AS 

SET NOCOUNT ON

SELECT	AuthorTypeID,
		AuthorTypeName,
		CreationDate,
		LastModifiedDate,
		CreationUserID,
		LastModifiedUserID
FROM	dbo.AuthorType
ORDER BY
		AuthorTypeName
		

