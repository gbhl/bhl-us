CREATE PROCEDURE [import].[ItemResolve]

@JournalTitle nvarchar(2000),
@ISSNValue nvarchar(250),
@ISBNValue nvarchar(250),
@OCLCValue nvarchar(250),
@Volume nvarchar(100),
@Issue nvarchar(10),
@Year nvarchar(20)

AS

BEGIN

SET NOCOUNT ON 

-- Get identifier IDs
DECLARE @EntityTypeTitle int

-- Try to find a matching title
CREATE TABLE #Title (TitleID int NOT NULL)

INSERT #Title 
SELECT	TitleID 
FROM	dbo.Title_Identifier ti INNER JOIN dbo.Identifier i ON ti.IdentifierID = i.IdentifierID
WHERE	IdentifierType = 'ISSN'
AND		IdentifierValue = @ISSNValue

INSERT #Title 
SELECT	TitleID 
FROM	dbo.Title_Identifier ti INNER JOIN dbo.Identifier i ON ti.IdentifierID = i.IdentifierID
WHERE	IdentifierType = 'ISBN'
AND		IdentifierValue = @ISBNValue

INSERT #Title 
SELECT	TitleID 
FROM	dbo.Title_Identifier ti INNER JOIN dbo.Identifier i ON ti.IdentifierID = i.IdentifierID
WHERE	IdentifierName = 'OCLC'
AND		IdentifierValue = @OCLCValue

INSERT #Title SELECT TitleID FROM dbo.Title WHERE FullTitle = @JournalTitle AND @JournalTitle <> '' AND PublishReady = 1

-- Now find the an item matching the title and the rest of the metadata
SELECT	b.BookID AS ItemID
FROM	dbo.Title t
		INNER JOIN dbo.ItemTitle it ON t.TitleID = it.TitleiD
		INNER JOIN dbo.Item i ON it.ItemID = i.ItemiD
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
WHERE	t.TitleID IN (SELECT TitleID FROM #Title)
AND		t.PublishReady = 1
AND		i.ItemStatusID = 40
AND		(RIGHT(SPACE(20) + @Year, 20) BETWEEN RIGHT(SPACE(20) + b.[StartYear], 20) AND RIGHT(SPACE(20) + b.EndYear, 20)
		OR b.[StartYear] = @Year
		OR b.EndYear = @Year
		OR ISNULL(@Year, '') = '')
AND		(RIGHT(SPACE(100) + @Volume, 10) BETWEEN RIGHT(SPACE(100) + b.StartVolume, 10) AND RIGHT(SPACE(100) + b.EndVolume, 10)
		OR b.StartVolume = @Volume
		OR b.EndVolume = @Volume
		OR ISNULL(@Volume, '') = '')
AND		(RIGHT(SPACE(10) + @Issue, 10) BETWEEN RIGHT(SPACE(10) + b.StartIssue, 10) AND RIGHT(SPACE(10) + b.EndIssue, 10)
		OR b.StartIssue = @Issue
		OR b.EndIssue = @Issue
		OR ISNULL(@Issue, '') = '')

END

GO
