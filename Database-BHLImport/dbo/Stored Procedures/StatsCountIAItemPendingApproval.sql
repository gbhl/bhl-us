
CREATE PROCEDURE [dbo].[StatsCountIAItemPendingApproval]

@AgeInDays INT = 45

AS
BEGIN

SET NOCOUNT ON

SELECT	COUNT(ItemID) AS [Number Of Items]
FROM	IAItem
WHERE	ItemStatusID = 20 
AND		DATEDIFF(DAY, ISNULL(IAAddedDate, CreatedDate), GETDATE()) > @AgeInDays

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure StatsCountIAItemPendingApproval. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

END


		
