CREATE PROCEDURE dbo.MaterialTypeUpdateAuto

@MaterialTypeID INT,
@MaterialTypeName NVARCHAR(60),
@MaterialTypeLabel NVARCHAR(60),
@MARCCode NCHAR(1)

AS 

SET NOCOUNT ON

UPDATE [dbo].[MaterialType]
SET
	[MaterialTypeName] = @MaterialTypeName,
	[MaterialTypeLabel] = @MaterialTypeLabel,
	[MARCCode] = @MARCCode
WHERE
	[MaterialTypeID] = @MaterialTypeID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.MaterialTypeUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END
GO
