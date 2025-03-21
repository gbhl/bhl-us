SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[BuildSynonyms]

@DBName NVARCHAR(50)

AS

BEGIN

SET NOCOUNT ON

-- Save a list of the synonym names and base objects
SELECT name, base_object_name INTO #synonyms FROM sys.synonyms

-- Drop the existing synonyms
DECLARE @synonym NVARCHAR(MAX)
SELECT TOP 1 @synonym = name FROM sys.synonyms

WHILE (@synonym IS NOT NULL)
BEGIN
	EXEC ('DROP SYNONYM dbo.' + @synonym)
	SET @synonym = NULL
	SELECT TOP 1 @synonym = name FROM sys.synonyms
END

-- Recreate the synonyms with the new base database name
DECLARE @name NVARCHAR(MAX)
DECLARE @baseObject NVARCHAR(MAX)
SELECT TOP 1 @name = name, @baseObject = base_object_name FROM #synonyms

WHILE (@name IS NOT NULL)
BEGIN
	-- Change the database of the base object
	SET @baseObject = REPLACE(@baseObject, SUBSTRING(@baseObject, 1, CHARINDEX('.', @baseObject)), '[' + @DBName + '].')
	EXEC ('CREATE SYNONYM dbo.' + @name + ' FOR ' + @baseObject)
	DELETE FROM #synonyms WHERE name = @name
	SELECT @name = NULL, @baseObject = NULL
	SELECT TOP 1 @name = name, @baseObject = base_object_name FROM #synonyms
END

DROP TABLE #synonyms

END


GO
