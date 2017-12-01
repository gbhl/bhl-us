CREATE PROCEDURE [import].[ItemResolve]

@JournalTitle nvarchar(2000),
@ISSNValue nvarchar(250),
@ISBNValue nvarchar(250),
@OCLCValue nvarchar(250),
@Volume nvarchar(100),
@Year nvarchar(20)

AS

BEGIN

SET NOCOUNT ON 

-- Get identifier IDs
DECLARE @ISSN int
DECLARE @ISBN int
DECLARE @OCLC int
DECLARE @EntityTypeTitle int
SELECT @ISSN = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'ISSN'
SELECT @ISBN = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'ISBN'
SELECT @OCLC = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'OCLC'

-- Try to find a matching title
CREATE TABLE #Title (TitleID int NOT NULL)

INSERT #Title SELECT TitleID FROM dbo.Title_Identifier WHERE IdentifierID = @ISSN AND IdentifierValue = @ISSNValue
INSERT #Title SELECT TitleID FROM dbo.Title_Identifier WHERE IdentifierID = @ISBN AND IdentifierValue = @ISBNValue
INSERT #Title SELECT TitleID FROM dbo.Title_Identifier WHERE IdentifierID = @OCLC AND IdentifierValue = @OCLCValue
INSERT #Title SELECT TitleID FROM dbo.Title WHERE FullTitle = @JournalTitle AND @JournalTitle <> '' AND PublishReady = 1

-- Now find the an item matching the title and the rest of the metadata
SELECT	i.ItemID
FROM	dbo.Title t
		INNER JOIN dbo.TitleItem ti ON t.TitleID = ti.TitleiD
		INNER JOIN dbo.Item i ON ti.ItemID = i.ItemiD
WHERE	t.TitleID IN (SELECT TitleID FROM #Title)
AND		t.PublishReady = 1
AND		i.ItemStatusID = 40
AND		(i.StartVolume = @Volume OR @Volume = '')
AND		(i.Year = @Year OR @Year = '')

END
