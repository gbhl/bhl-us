CREATE PROCEDURE [dbo].[IdentifierSelectAll]

AS 

SET NOCOUNT ON

SELECT	IdentifierID,
		IdentifierName,
		Prefix,
		PatternExpression,
		PatternDescription,
		MaximumPerEntity
FROM	dbo.Identifier
ORDER BY
		IdentifierName

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IdentifierSelectAll. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
