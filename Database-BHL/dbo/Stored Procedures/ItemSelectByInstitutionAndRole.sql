﻿CREATE PROCEDURE [dbo].[ItemSelectByInstitutionAndRole]

@InstitutionCode nvarchar(10),
@InstitutionRoleID int,
@NumRows int = 100,
@PageNum int = 1,
@SortColumn nvarchar(150) = 'CreationDate',
@SortDirection nvarchar(4) = 'desc'

AS 

BEGIN

SET NOCOUNT ON

DECLARE @TotalItems INT

-- Create a temp table for the initial data set
CREATE TABLE #Step1
	(
	ItemID int NOT NULL,
	BarCode nvarchar(50) NOT NULL,
	TitleName nvarchar(2000) NOT NULL,
	SortTitle nvarchar(60) NOT NULL,
	Volume nvarchar(100) NOT NULL,
	StartVolume nvarchar(10) NOT NULL,
	StartIssue nvarchar(10) NOT NULL,
	StartNumber nvarchar(10) NOT NULL,
	StartSeries nvarchar(10) NOT NULL,
	Year nvarchar(20) NOT NULL,
	CreatorTextString nvarchar(max) NOT NULL,
	CopyrightStatus nvarchar(max) NOT NULL,
	Rights nvarchar(max) NOT NULL,
	LicenseUrl nvarchar(max) NOT NULL,
	DueDiligence nvarchar(max) NOT NULL,
	CreationDate datetime NOT NULL
	)
	
-- Get the initial data set
INSERT #Step1
SELECT	i.ItemID,
		i.BarCode,
		t.FullTitle AS TitleName, 
		t.SortTitle,
		ISNULL(i.Volume, ''),
		i.StartVolume,
		i.StartIssue,
		i.StartNumber,
		i.StartSeries,
		CASE WHEN ISNULL(i.Year, '') = '' THEN ISNULL(CONVERT(nvarchar(20), t.StartYear), '') ELSE i.Year END,
		c.Authors AS CreatorTextString,
		i.CopyrightStatus,
		i.Rights,
		i.LicenseUrl,
		i.DueDiligence,
		i.CreationDate
FROM	dbo.Item i WITH (NOLOCK)
		INNER JOIN dbo.Title t WITH (NOLOCK)ON i.PrimaryTitleID = t.TitleID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID AND i.ItemID = c.ItemID
		INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
WHERE	ii.InstitutionCode = @InstitutionCode
AND		ii.InstitutionRoleID = @InstitutionRoleID

-- Create a temp table for the second step
CREATE TABLE #Step2
	(
	ItemID int NOT NULL,
	BarCode nvarchar(50) NOT NULL,
	TitleName nvarchar(2000) NOT NULL,
	SortTitle nvarchar(60) NOT NULL,
	Volume nvarchar(100) NOT NULL,
	StartVolume nvarchar(10) NOT NULL,
	StartIssue nvarchar(10) NOT NULL,
	StartNumber nvarchar(10) NOT NULL,
	StartSeries nvarchar(10) NOT NULL,
	Year nvarchar(20) NOT NULL,
	CreatorTextString nvarchar(max) NOT NULL,
	CopyrightStatus nvarchar(max) NOT NULL,
	Rights nvarchar(max) NOT NULL,
	LicenseUrl nvarchar(max) NOT NULL,
	DueDiligence nvarchar(max) NOT NULL,
	CreationDate datetime NOT NULL,
	RowNumber int NOT NULL
	)

-- Add a row number to the data set, first sorting by the specified field
IF (@SortColumn = 'TitleName' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	*,
			ROW_NUMBER() OVER (
				ORDER BY 
					SortTitle, 
					Year,
					RIGHT(SPACE(10) + 
						CASE WHEN ISNUMERIC(StartVolume) = 1 
						THEN CONVERT(NVARCHAR(10), CONVERT(int, StartVolume)) 
						ELSE StartVolume END, 10),
					RIGHT(SPACE(10) + 
						CASE WHEN ISNUMERIC(StartIssue) = 1 
						THEN CONVERT(NVARCHAR(10), CONVERT(int, StartIssue)) 
						ELSE StartIssue END, 10),
					RIGHT(SPACE(10) + 
						CASE WHEN ISNUMERIC(StartNumber) = 1 
						THEN CONVERT(NVARCHAR(10), CONVERT(int, StartNumber)) 
						ELSE StartNumber END, 10),
					RIGHT(SPACE(10) + 
						CASE WHEN ISNUMERIC(StartSeries) = 1 
						THEN CONVERT(NVARCHAR(10), CONVERT(int, StartSeries)) 
						ELSE StartSeries END, 10)
				)
	FROM	#Step1
END
IF (@SortColumn = 'TitleName' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	*, 
			ROW_NUMBER() OVER (
				ORDER BY 
					SortTitle DESC, 
					Year,
					RIGHT(SPACE(10) + 
						CASE WHEN ISNUMERIC(StartVolume) = 1 
						THEN CONVERT(NVARCHAR(10), CONVERT(int, StartVolume)) 
						ELSE StartVolume END, 10) DESC,
					RIGHT(SPACE(10) + 
						CASE WHEN ISNUMERIC(StartIssue) = 1 
						THEN CONVERT(NVARCHAR(10), CONVERT(int, StartIssue)) 
						ELSE StartIssue END, 10) DESC,
					RIGHT(SPACE(10) + 
						CASE WHEN ISNUMERIC(StartNumber) = 1 
						THEN CONVERT(NVARCHAR(10), CONVERT(int, StartNumber)) 
						ELSE StartNumber END, 10) DESC,
					RIGHT(SPACE(10) + 
						CASE WHEN ISNUMERIC(StartSeries) = 1 
						THEN CONVERT(NVARCHAR(10), CONVERT(int, StartSeries)) 
						ELSE StartSeries END, 10) DESC
				)
	FROM	#Step1
END
IF (@SortColumn = 'CreationDate' AND LOWER(@SortDirection) = 'asc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY CreationDate)
	FROM	#Step1
END
IF (@SortColumn = 'CreationDate' AND LOWER(@SortDirection) = 'desc')
BEGIN
	INSERT	#Step2
	SELECT	*, ROW_NUMBER() OVER (ORDER BY CreationDate DESC)
	FROM	#Step1
END

-- Count the total number of pages
SELECT @TotalItems = COUNT(*) FROM #Step2

-- Return the final result set
SELECT TOP (@NumRows) 
		ItemID,
		BarCode,
		TitleName,
		Volume,
		Year,
		CreatorTextString,
		CopyrightStatus,
		Rights,
		LicenseUrl,
		DueDiligence,
		CreationDate,
		@TotalItems AS TotalItems
FROM	#Step2
WHERE	RowNumber > (@PageNum - 1) * @NumRows
ORDER BY 
		RowNumber

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemSelectByInstitutionAndRole. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

END