CREATE PROCEDURE [dbo].[KeywordSelectByKeyword]

@Keyword nvarchar(50)

AS 

SET NOCOUNT ON

SELECT	KeywordID,
		Keyword,
		CreationDate,
		CreationUserID,
		LastModifiedDate,
		LastModifiedUserID
FROM	dbo.Keyword
WHERE	Keyword = @Keyword


