
CREATE PROCEDURE dbo.StatsSelectIAItemPendingApprovalGroupByAge

@AgeInDays INT = NULL

AS
BEGIN

SET NOCOUNT ON

SELECT	DATEDIFF(DAY, IAAddedDate, GETDATE()) AS [Age In Days], 
		COUNT(*) AS [Number Of Items]
FROM	dbo.IAItem 
WHERE	ItemStatusID = 20 
AND		(DATEDIFF(DAY, IAAddedDate, getdate()) > @AgeInDays OR @AgeInDays IS NULL)
AND		IAAddedDate IS NOT NULL
GROUP BY 
		DATEDIFF(DAY, IAAddedDate, GETDATE())
ORDER BY
		[Age In Days]

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure StatsSelectIAItemPendingApprovalGroupByAge. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

END


		
