

CREATE PROCEDURE [dbo].[InstitutionSelectByItemID]

@ItemID int

AS 

SET NOCOUNT ON

SELECT 

	[Institution].[InstitutionCode],
	[Institution].[InstitutionName],
	[Institution].[Note],
	[Institution].[InstitutionUrl]

FROM [dbo].[Item]
JOIN [dbo].[Institution] ON [Item].[InstitutionCode] = [Institution].[InstitutionCode]

WHERE
	[Item].[ItemID] = @ItemID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure InstitutionSelectByItemID. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

