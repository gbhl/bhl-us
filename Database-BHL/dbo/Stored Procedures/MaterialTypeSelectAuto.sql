CREATE PROCEDURE [dbo].[MaterialTypeSelectAuto]

@MaterialTypeID INT

AS 

SET NOCOUNT ON

SELECT	
	[MaterialTypeID],
	[MaterialTypeName],
	[MaterialTypeLabel],
	[MARCCode]
FROM	
	[dbo].[MaterialType]
WHERE	
	[MaterialTypeID] = @MaterialTypeID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.MaterialTypeSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
