CREATE PROCEDURE [dbo].[IAItemSelectPendingApproval]

@AgeInDays INT = 45

AS
BEGIN

SET NOCOUNT ON

SELECT DISTINCT
		i.ItemID,
		i.ItemStatusID,
		s.Status,
		IAIdentifier,
		Sponsor,
		ISNULL(COALESCE(mm.DCElementValue, md.DCElementValue), '') AS HoldingInstitution,
		CallNumber,
		ImageCount,
		IdentifierAccessUrl,
		Volume,
		[Year],
		IAAddedDate,
		CASE 
			WHEN LEN(ScanDate) = 14 THEN SUBSTRING(ScanDate, 1, 4) + '-' + SUBSTRING(ScanDate, 5, 2) + '-' + SUBSTRING(ScanDate, 7, 2) + ' ' + 
				SUBSTRING(ScanDate, 9, 2) + ':' + SUBSTRING(ScanDate, 11, 2) + ':' + SUBSTRING(ScanDate, 13, 2)
			WHEN LEN(ScanDate) = 8 THEN SUBSTRING(ScanDate, 1, 4) + '-' + SUBSTRING(ScanDate, 5, 2) + '-' + SUBSTRING(ScanDate, 7, 2) 
			WHEN LEN(ScanDate) = 0 THEN NULL
			ELSE ScanDate
		END AS ScanDate,
		IADateStamp,
		LastXMLDataHarvestDate,
		i.CreatedDate,
		i.LastModifiedDate,
		LTRIM(uc.FirstName + ' ' + uc.LastName) AS CreatedUser,
		LTRIM(um.FirstName + ' ' + um.LastName) AS LastModifiedUser
FROM	IAItem i 
		INNER JOIN IAItemStatus s ON i.ItemStatusID = s.ItemStatusID
		LEFT JOIN dbo.IADCMetadata md ON i.ItemID = md.ItemID AND md.DCElementName = 'contributor' AND md.[Source] = 'DC'
		LEFT JOIN dbo.IADCMetadata mm ON i.ItemID = mm.ItemID AND mm.DCElementName = 'contributor' AND mm.[Source] = 'META'
		LEFT JOIN BHLAspNetUsers uc ON i.CreatedUserID = uc.Id
		LEFT JOIN BHLAspNetUsers um ON i.LastModifiedUserID = um.Id
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

GO
