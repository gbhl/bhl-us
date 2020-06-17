CREATE PROCEDURE [dbo].[IdentifierSelectByIDType]

@IDTypeName nvarchar(50)

AS 

SET NOCOUNT ON

SELECT	id.IdentifierID,
		id.IdentifierName
FROM	dbo.Identifier id
		INNER JOIN dbo.IdentifierIDType iit ON id.IdentifierID = iit.IdentifierID
		INNER JOIN dbo.IDType idt ON iit.IDTypeID = idt.IDTypeID
WHERE	idt.IDTypeName = @IDTypeName
ORDER BY
		id.IdentifierName

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IdentifierSelectByType. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
