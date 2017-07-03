CREATE PROCEDURE dbo.MaterialTypeInsertAuto

@MaterialTypeID INT OUTPUT,
@MaterialTypeName NVARCHAR(60),
@MaterialTypeLabel NVARCHAR(60),
@MARCCode NCHAR(1)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[MaterialType]
( 	[MaterialTypeName],
	[MaterialTypeLabel],
	[MARCCode] )
VALUES
( 	@MaterialTypeName,
	@MaterialTypeLabel,
	@MARCCode )

SET @MaterialTypeID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.MaterialTypeInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[MaterialTypeID],
		[MaterialTypeName],
		[MaterialTypeLabel],
		[MARCCode]	
	FROM [dbo].[MaterialType]
	WHERE
		[MaterialTypeID] = @MaterialTypeID
	
	RETURN -- insert successful
END
GO
