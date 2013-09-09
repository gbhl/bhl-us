
CREATE PROCEDURE [dbo].[IAItemSelectPendingApproval]

@AgeInDays INT = 45

AS
BEGIN

SET NOCOUNT ON

SELECT	ItemID,
		i.ItemStatusID,
		s.Status,
		IAIdentifier,
		Sponsor,
		SponsorName,
		ScanningCenter,
		CallNumber,
		ImageCount,
		IdentifierAccessUrl,
		Volume,
		Note,
		ScanOperator,
		ScanDate,
		IAAddedDate,
		LastXMLDataHarvestDate,
		i.CreatedDate
FROM	IAItem i INNER JOIN IAItemStatus s
			ON i.ItemStatusID = s.ItemStatusID
WHERE	i.ItemStatusID = 20 
AND		DATEDIFF(DAY, ISNULL(IAAddedDate, i.CreatedDate), GETDATE()) > @AgeInDays
ORDER BY 
		IAAddedDate

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAItemSelectPendingApproval. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

END


		
