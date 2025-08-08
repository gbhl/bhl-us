CREATE PROCEDURE dbo.WDEntityIdentifierInsert
	@BHLEntityType NVARCHAR(20),
	@BHLEntityID INT,
	@IdentifierType NVARCHAR(40),
	@IdentifierValue NVARCHAR(125),
	@HarvestDate DATETIME
AS
BEGIN

IF NOT EXISTS(
	SELECT	IdentifierValue 
	FROM	dbo.WDEntityIdentifier 
	WHERE	BHLEntityType = @BHLEntityType
	AND		BHLEntityID = @BHLEntityID
	AND		IdentifierType = @IdentifierType
	AND		IdentifierValue = @IdentifierValue
	)
BEGIN
	INSERT	dbo.WDEntityIdentifier
		(
		BHLEntityType,
		BHLEntityID,
		IdentifierType,
		IdentifierValue,
		HarvestDate
		)
	VALUES
		(
		@BHLEntityType,
		@BHLEntityID,
		@IdentifierType,
		@IdentifierValue,
		@HarvestDate
		)
END

END
GO

