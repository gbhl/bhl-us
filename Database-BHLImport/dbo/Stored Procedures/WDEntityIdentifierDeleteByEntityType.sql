CREATE OR ALTER PROCEDURE dbo.WDEntityIdentifierDeleteByEntityType

@EntityType nvarchar(20)

AS
BEGIN

DELETE FROM dbo.WDEntityIdentifier WHERE BHLEntityType = @EntityType

END
GO
