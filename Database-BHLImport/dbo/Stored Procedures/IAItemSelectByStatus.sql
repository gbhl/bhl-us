CREATE PROCEDURE [dbo].[IAItemSelectByStatus]

@ItemStatusID int,
@IAIdentifier nvarchar(200) = '',
@NumRows int = 100,
@PageNum int = 1,
@SortColumn nvarchar(150) = 'IAIdentifier',
@SortDirection nvarchar(4) = 'ASC'

AS 

SET NOCOUNT ON

DECLARE @TotalItems INT

CREATE TABLE #Step1
(
	ItemID int NOT NULL
)

-- Get the initial data set
INSERT #Step1
SELECT	i.ItemID
FROM	dbo.IAItem i
WHERE	(i.ItemStatusID = @ItemStatusID OR @ItemStatusID = -1)
AND		(i.IAIdentifier LIKE @IAIdentifier + '%' OR @IAIdentifier = '')

-- Create a temp table for the second step
-- Create a temp table for the second step
CREATE TABLE #Step2
	(
	ItemID int NOT NULL
	,RowNumber INT NOT NULL
	)

-- Add a row number to the data set, first sorting by the specified field
IF (@SortColumn = 'IAIdentifier' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	t.*, ROW_NUMBER() OVER (ORDER BY i.IAIdentifier)
	FROM	#Step1 t
			INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
END
IF (@SortColumn = 'IAIdentifier' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	t.*, ROW_NUMBER() OVER (ORDER BY i.IAIdentifier DESC)
	FROM	#Step1 t
			INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
END
IF (@SortColumn = 'ExternalStatus' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	t.*, ROW_NUMBER() OVER (ORDER BY ExternalStatus)
	FROM	#Step1 t
			INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
END
IF (@SortColumn = 'ExternalStatus' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	t.*, ROW_NUMBER() OVER (ORDER BY ExternalStatus DESC)
	FROM	#Step1 t
			INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
END
IF (@SortColumn = 'Status' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	t.*, ROW_NUMBER() OVER (ORDER BY [Status])
	FROM	#Step1 t
			INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
			INNER JOIN dbo.IAItemStatus s ON i.ItemStatusID = s.ItemStatusID
END
IF (@SortColumn = 'Status' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	t.*, ROW_NUMBER() OVER (ORDER BY [Status] DESC)
	FROM	#Step1 t
			INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
			INNER JOIN dbo.IAItemStatus s ON i.ItemStatusID = s.ItemStatusID
END
IF (@SortColumn = 'HoldingInstitution' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	t.*, ROW_NUMBER() OVER (ORDER BY ISNULL(COALESCE(mm.DCElementValue, md.DCElementValue), ''))
	FROM	#Step1 t
			INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
			LEFT JOIN dbo.IADCMetadata md ON i.ItemID = md.ItemID AND md.DCElementName = 'contributor' AND md.[Source] = 'DC'
			LEFT JOIN dbo.IADCMetadata mm ON i.ItemID = mm.ItemID AND mm.DCElementName = 'contributor' AND mm.[Source] = 'META'
END
IF (@SortColumn = 'HoldingInstitution' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	t.*, ROW_NUMBER() OVER (ORDER BY ISNULL(COALESCE(mm.DCElementValue, md.DCElementValue), '') DESC)
	FROM	#Step1 t
			INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
			LEFT JOIN dbo.IADCMetadata md ON i.ItemID = md.ItemID AND md.DCElementName = 'contributor' AND md.[Source] = 'DC'
			LEFT JOIN dbo.IADCMetadata mm ON i.ItemID = mm.ItemID AND mm.DCElementName = 'contributor' AND mm.[Source] = 'META'
END
IF (@SortColumn = 'IAAddedDate' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	t.*, ROW_NUMBER() OVER (ORDER BY IAAddedDate)
	FROM	#Step1 t
			INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
END
IF (@SortColumn = 'IAAddedDate' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	t.*, ROW_NUMBER() OVER (ORDER BY IAAddedDate DESC)
	FROM	#Step1 t
			INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
END
IF (@SortColumn = 'ScanDate' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	t.*, ROW_NUMBER() OVER (ORDER BY 
				ISNULL(
					CASE 
						WHEN LEN(ScanDate) = 14 THEN SUBSTRING(ScanDate, 1, 4) + '-' + SUBSTRING(ScanDate, 5, 2) + '-' + SUBSTRING(ScanDate, 7, 2) + ' ' + 
							SUBSTRING(ScanDate, 9, 2) + ':' + SUBSTRING(ScanDate, 11, 2) + ':' + SUBSTRING(ScanDate, 13, 2)
						WHEN LEN(ScanDate) = 8 THEN SUBSTRING(ScanDate, 1, 4) + '-' + SUBSTRING(ScanDate, 5, 2) + '-' + SUBSTRING(ScanDate, 7, 2) 
						WHEN LEN(ScanDate) = 0 THEN NULL
						ELSE ScanDate
					END, '')
				)
	FROM	#Step1 t
			INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
END
IF (@SortColumn = 'ScanDate' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	t.*, ROW_NUMBER() OVER (ORDER BY
				ISNULL(
					CASE 
						WHEN LEN(ScanDate) = 14 THEN SUBSTRING(ScanDate, 1, 4) + '-' + SUBSTRING(ScanDate, 5, 2) + '-' + SUBSTRING(ScanDate, 7, 2) + ' ' + 
							SUBSTRING(ScanDate, 9, 2) + ':' + SUBSTRING(ScanDate, 11, 2) + ':' + SUBSTRING(ScanDate, 13, 2)
						WHEN LEN(ScanDate) = 8 THEN SUBSTRING(ScanDate, 1, 4) + '-' + SUBSTRING(ScanDate, 5, 2) + '-' + SUBSTRING(ScanDate, 7, 2) 
						WHEN LEN(ScanDate) = 0 THEN NULL
						ELSE ScanDate
					END, '') DESC
				)
	FROM	#Step1 t
			INNER JOIN dbo.IAItem i on t.ItemID = i.ItemID
END
IF (@SortColumn = 'IADateStamp' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	t.*, ROW_NUMBER() OVER (ORDER BY IADateStamp)
	FROM	#Step1 t
			INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
END
IF (@SortColumn = 'IADateStamp' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	t.*, ROW_NUMBER() OVER (ORDER BY IADateStamp DESC)
	FROM	#Step1 t
			INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
END
IF (@SortColumn = 'LastXMLDataHarvestDate' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	t.*, ROW_NUMBER() OVER (ORDER BY LastXMLDataHarvestDate)
	FROM	#Step1 t
			INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
END
IF (@SortColumn = 'LastXMLDataHarvestDate' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	t.*, ROW_NUMBER() OVER (ORDER BY LastXMLDataHarvestDate DESC)
	FROM	#Step1 t
			INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
END
IF (@SortColumn = 'LastProductionDate' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	t.*, ROW_NUMBER() OVER (ORDER BY LastProductionDate)
	FROM	#Step1 t
			INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
END
IF (@SortColumn = 'LastProductionDate' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	t.*, ROW_NUMBER() OVER (ORDER BY LastProductionDate DESC)
	FROM	#Step1 t
			INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
END
IF (@SortColumn = 'CreatedDate' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	t.*, ROW_NUMBER() OVER (ORDER BY CreatedDate)
	FROM	#Step1 t
			INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
END
IF (@SortColumn = 'CreatedDate' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	t.*, ROW_NUMBER() OVER (ORDER BY CreatedDate DESC)
	FROM	#Step1 t
			INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
END
IF (@SortColumn = 'LastModifiedDate' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	t.*, ROW_NUMBER() OVER (ORDER BY LastModifiedDate)
	FROM	#Step1 t
			INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
END
IF (@SortColumn = 'LastModifiedDate' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	t.*, ROW_NUMBER() OVER (ORDER BY LastModifiedDate DESC)
	FROM	#Step1 t
			INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
END
IF (@SortColumn = 'CreatedUser' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	t.*, ROW_NUMBER() OVER (ORDER BY CreatedUser)
	FROM	#Step1 t
			INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
			LEFT JOIN dbo.BHLAspNetUsers uc ON i.CreatedUserID = uc.Id
			LEFT JOIN dbo.BHLAspNetUsers um ON i.LastModifiedUserID = um.Id
END
IF (@SortColumn = 'CreatedUser' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	t.*, ROW_NUMBER() OVER (ORDER BY CreatedUser DESC)
	FROM	#Step1 t
			INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
			LEFT JOIN dbo.BHLAspNetUsers uc ON i.CreatedUserID = uc.Id
			LEFT JOIN dbo.BHLAspNetUsers um ON i.LastModifiedUserID = um.Id
END
IF (@SortColumn = 'LastModifiedUser' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	t.*, ROW_NUMBER() OVER (ORDER BY LastModifiedUser)
	FROM	#Step1 t
			INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
			LEFT JOIN dbo.BHLAspNetUsers uc ON i.CreatedUserID = uc.Id
			LEFT JOIN dbo.BHLAspNetUsers um ON i.LastModifiedUserID = um.Id
END
IF (@SortColumn = 'LastModifiedUser' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	t.*, ROW_NUMBER() OVER (ORDER BY LastModifiedUser DESC)
	FROM	#Step1 t
			INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
			LEFT JOIN dbo.BHLAspNetUsers uc ON i.CreatedUserID = uc.Id
			LEFT JOIN dbo.BHLAspNetUsers um ON i.LastModifiedUserID = um.Id
END

-- Count the total number of pages
SELECT @TotalItems = COUNT(*) FROM #Step2

-- Return the final result set
SELECT TOP (@NumRows) 
		t.ItemID
		,i.IAIdentifier
		,ISNULL(i.ExternalStatus, '') AS ExternalStatus
		,s.[Status]
		,ISNULL(i.ShortTitle, '') AS ShortTitle
		,ISNULL(i.Volume, '') AS Volume
		,i.Year
		,i.ImageCount
		,ISNULL(mm.DCElementValue, '') AS HoldingInstitution
		--,CONVERT(nvarchar(500), '') AS HoldingInstitution
		,ISNULL(i.Sponsor, '') AS Sponsor
		,ISNULL(i.ScanningInstitution, '') AS ScanningInstitution
		,ISNULL(i.RightsHolder, '') AS RightsHolder
		,ISNULL(i.Note, '') AS Note
		,ISNULL(i.LicenseUrl, '') AS LicenseUrl
		,ISNULL(i.Rights, '') AS Rights
		,ISNULL(i.DueDiligence, '') AS DueDiligence
		,ISNULL(i.PossibleCopyrightStatus, '') AS PossibleCopyrightStatus
		,i.VirtualTitleID
		,i.VirtualVolume
		,i.IAAddedDate
		,ISNULL(
			CASE 
				WHEN LEN(ScanDate) = 14 THEN SUBSTRING(ScanDate, 1, 4) + '-' + SUBSTRING(ScanDate, 5, 2) + '-' + SUBSTRING(ScanDate, 7, 2) + ' ' + 
					SUBSTRING(ScanDate, 9, 2) + ':' + SUBSTRING(ScanDate, 11, 2) + ':' + SUBSTRING(ScanDate, 13, 2)
				WHEN LEN(ScanDate) = 8 THEN SUBSTRING(ScanDate, 1, 4) + '-' + SUBSTRING(ScanDate, 5, 2) + '-' + SUBSTRING(ScanDate, 7, 2) 
				WHEN LEN(ScanDate) = 0 THEN NULL
				ELSE ScanDate
			END, '') AS ScanDate
		,i.IADateStamp
		,i.LastXMLDataHarvestDate
		,i.LastProductionDate
		,i.CreatedDate
		,i.LastModifiedDate
		,LTRIM(uc.FirstName + ' ' + uc.LastName) AS CreatedUser
		,LTRIM(um.FirstName + ' ' + um.LastName) AS LastModifiedUser
		,@TotalItems AS TotalItems
		,RowNumber
INTO	#Step3
FROM	#Step2 t
		INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
		INNER JOIN dbo.IAItemStatus s ON i.ItemStatusID = s.ItemStatusID
		LEFT JOIN dbo.IADCMetadata mm ON t.ItemID = mm.ItemID AND mm.DCElementName = 'contributor' AND mm.[Source] = 'META'
		LEFT JOIN dbo.BHLAspNetUsers uc ON i.CreatedUserID = uc.Id
		LEFT JOIN dbo.BHLAspNetUsers um ON i.LastModifiedUserID = um.Id
WHERE	RowNumber > (@PageNum - 1) * @NumRows
ORDER BY 
		RowNumber

UPDATE	t
SET		HoldingInstitution = ISNULL(md.DCElementValue, '')
FROM	#Step3 t
		INNER JOIN dbo.IADCMetadata md ON t.ItemID = md.ItemID AND md.DCElementName = 'contributor' AND md.[Source] = 'DC'
WHERE	t.HoldingInstitution = ''

SELECT	ItemID
		,IAIdentifier
		,ExternalStatus
		,[Status]
		,ShortTitle
		,Volume
		,Year
		,ImageCount
		,HoldingInstitution
		,Sponsor
		,ScanningInstitution
		,RightsHolder
		,Note
		,LicenseUrl
		,Rights
		,DueDiligence
		,PossibleCopyrightStatus
		,VirtualTitleID
		,VirtualVolume
		,IAAddedDate
		,ScanDate
		,IADateStamp
		,LastXMLDataHarvestDate
		,LastProductionDate
		,CreatedDate
		,LastModifiedDate
		,CreatedUser
		,LastModifiedUser
		,TotalItems
FROM	#Step3
ORDER BY RowNumber

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAItemSelectByStatus. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
