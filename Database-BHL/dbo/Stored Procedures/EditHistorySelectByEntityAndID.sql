CREATE PROCEDURE dbo.EditHistorySelectByEntityAndID

@EntitySchema nvarchar(50),
@EntityName nvarchar(50),
@EntityID nvarchar(100)

AS

BEGIN

SET NOCOUNT ON 

DECLARE @EntityColumn nvarchar(100) = NULL
DECLARE @Sql nvarchar(max)

-- GET THE PRIMARY KEY COLUMN NAMES
--		--> Consider just making this a parameter, rather than performing this lookup (which relies on the assumption that the PK column is in ordinal position 1)
IF (@EntityID IS NOT NULL)
BEGIN
	SELECT	@EntityColumn = c.COLUMN_NAME
	FROM	INFORMATION_SCHEMA.TABLE_CONSTRAINTS p
			INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE c ON c.TABLE_NAME = p.TABLE_NAME AND c.CONSTRAINT_NAME = p.CONSTRAINT_NAME
			INNER JOIN INFORMATION_SCHEMA.COLUMNS cls ON c.TABLE_NAME = cls.TABLE_NAME AND c.COLUMN_NAME = cls.COLUMN_NAME
	WHERE	CONSTRAINT_TYPE = 'PRIMARY KEY'
	AND		c.TABLE_SCHEMA = @EntitySchema
	AND		c.TABLE_NAME = @EntityName
	AND		c.ORDINAL_POSITION = 1
END

-- TEMP TABLE TO POPULATE WITH EDIT HISTORY
CREATE TABLE #History
	(
	EditDate datetime NULL,
	EntityName nvarchar(50) NOT NULL,
	Operation nchar(1) NOT NULL,
	FirstName nvarchar(max) NULL,
	LastName nvarchar(max) NULL,
	Email nvarchar(256) NULL
	)

-- GET THE INITIAL CREATION DATE/USER
IF EXISTS (	SELECT	COLUMN_NAME 
			FROM	INFORMATION_SCHEMA.COLUMNS
			WHERE	TABLE_SCHEMA = @EntitySchema
			AND		TABLE_NAME = @EntityName
			AND		COLUMN_NAME = 'CreationDate'
			)
BEGIN
	IF EXISTS (	SELECT	COLUMN_NAME 
				FROM	INFORMATION_SCHEMA.COLUMNS
				WHERE	TABLE_SCHEMA = @EntitySchema
				AND		TABLE_NAME = @EntityName
				AND		COLUMN_NAME = 'CreationUserID'
				)
	BEGIN
		SET @Sql = 'SELECT CONVERT(datetime, CONVERT(nvarchar(50), CreationDate, 120)), ''' + @EntitySchema + '.' + @EntityName + 
					''', ''I'', u.FirstName, u.LastName, u.Email ' + 
				'FROM ' + @EntitySchema + '.' + @EntityName + ' x ' + 
						'LEFT JOIN dbo.AspNetUsers u ON x.CreationUserID = u.Id ' + 
				'WHERE CONVERT(nvarchar(max), x.' + @EntityColumn + ') = ''' + @EntityID + ''''
		INSERT #History EXEC (@Sql)
	END
	ELSE
	BEGIN
		SET @Sql = 'SELECT CONVERT(datetime, CONVERT(nvarchar(50), CreationDate, 120)), ''' + @EntitySchema + '.' + @EntityName + 
					''', ''I'', NULL, NULL, NULL ' + 
				'FROM ' + @EntitySchema + '.' + @EntityName + ' x ' + 
				'WHERE CONVERT(nvarchar(max), x.' + @EntityColumn + ') = ''' + @EntityID + ''''
		INSERT #History EXEC (@Sql)
	END
END

-- GET THE LAST MODIFIED DATE/USER
IF EXISTS (	SELECT	COLUMN_NAME 
			FROM	INFORMATION_SCHEMA.COLUMNS
			WHERE	TABLE_SCHEMA = @EntitySchema
			AND		TABLE_NAME = @EntityName
			AND		COLUMN_NAME = 'LastModifiedDate'
			)
BEGIN
	IF EXISTS (	SELECT	COLUMN_NAME 
				FROM	INFORMATION_SCHEMA.COLUMNS
				WHERE	TABLE_SCHEMA = @EntitySchema
				AND		TABLE_NAME = @EntityName
				AND		COLUMN_NAME = 'LastModifiedUserID'
				)
	BEGIN
		SET @Sql = 'SELECT CONVERT(datetime, CONVERT(nvarchar(50), LastModifiedDate, 120)), ''' + @EntitySchema + '.' + @EntityName + 
					''', ''U'', u.FirstName, u.LastName, u.Email ' + 
				'FROM ' + @EntitySchema + '.' + @EntityName + ' x ' + 
						'LEFT JOIN dbo.AspNetUsers u ON x.LastModifiedUserID = u.Id ' + 
				'WHERE ISNULL(x.CreationDate, ''1/1/1980'') <> ISNULL(x.LastModifiedDate, ''1/1/1980'') ' +
				'AND CONVERT(nvarchar(max), x.' + @EntityColumn + ') = ''' + @EntityID + ''''
		INSERT #History EXEC (@Sql)
	END
	ELSE
	BEGIN
		SET @Sql = 'SELECT CONVERT(datetime, CONVERT(nvarchar(50), LastModifiedDate, 120)), ''' + @EntitySchema + '.' + @EntityName + 
					''', ''U'', NULL, NULL, NULL ' + 
				'FROM ' + @EntitySchema + '.' + @EntityName + ' x ' + 
				'WHERE ISNULL(x.CreationDate, ''1/1/1980'') <> ISNULL(x.LastModifiedDate, ''1/1/1980'') ' +
				'AND CONVERT(nvarchar(max), x.' + @EntityColumn + ') = ''' + @EntityID + ''''
		INSERT #History EXEC (@Sql)
	END
END

-- GET THE REST OF THE HISTORY
INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), b.EntityName, b.Operation, u.FirstName, u.LastName, u.Email
FROM	BHLAuditArchive.audit.AuditBasic b
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
WHERE	b.EntityName = @EntitySchema + '.' + @EntityName
AND		b.EntityKey1 = @EntityID
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), b.EntityName, b.Operation, u.FirstName, u.LastName, u.Email
FROM	audit.AuditBasic b
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
WHERE	b.EntityName = @EntitySchema + '.' + @EntityName
AND		b.EntityKey1 = @EntityID

-- FINAL RESULT SET
SELECT	EditDate, EntityName, Operation, MAX(FirstName) AS FirstName, MAX(LastName) AS LastName, MAX(Email) AS Email
FROM	#History 
WHERE	Operation IN ('I', 'U', 'D')
GROUP BY EditDate, EntityName, Operation 
ORDER BY EditDate DESC

DROP TABLE #History

END
