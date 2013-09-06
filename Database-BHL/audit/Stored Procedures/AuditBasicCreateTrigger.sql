CREATE PROC [audit].[AuditBasicCreateTrigger] (
   @SchemaName SYSNAME = 'dbo',
   @TableName SYSNAME,
   @PKColumnName1 SYSNAME,
   @PKColumnName2 SYSNAME,
   @PKColumnName3 SYSNAME,
   @BuildOnly NCHAR(1) = ''
) 
AS 
--------------------------------------------------------------------
--
-- Description:
--
--	Script to create Audit triggers on the specified table
--
-- Parameters:
--
--	@SchemaName - database schema for the trigger(s) target table
--	@TableName - table on which to build the trigger(s)
--	@PKColumnName1 - key column to capture in audit table
--	@PKColumnName2 - key column to capture in audit table
--	@PKColumnName3 - key column to capture in audit table
--	@BuildOnly - specifies which triggers to build
--		'' to build insert, update, and delete triggers
--		'I' to build only the insert trigger
--		'U' to build only the update trigger
--		'D' to build only the delete trigger
--
--------------------------------------------------------------------
BEGIN

SET NOCOUNT ON

DECLARE	@SQL NVARCHAR(max)
DECLARE @InsertUserIDColumn NVARCHAR(256)
DECLARE @UpdateUserIDColumn NVARCHAR(256)

-- Check whether the target table includes the CreationUserID column.
-- If so, use it to record the application user id in insert triggers.
IF EXISTS(
	SELECT	c.name
	FROM	sys.tables o 
			INNER JOIN sys.schemas s ON o.schema_id = s.schema_id
			INNER JOIN sys.columns c ON o.object_id = c.object_id
	WHERE	o.type = 'U' 
	AND		o.name = @TableName
	AND		s.name = @SchemaName
	AND		c.name = 'CreationUserID'
	)
	SET @InsertUserIDColumn = 'Inserted.CreationUserID'
ELSE
	SET @InsertUserIDColumn = 'NULL'

-- Check whether the target table includes the LastModifiedUserID column.
-- If so, use it to record the application user id in update triggers.
IF EXISTS(
	SELECT	c.name
	FROM	sys.tables o 
			INNER JOIN sys.schemas s ON o.schema_id = s.schema_id
			INNER JOIN sys.columns c ON o.object_id = c.object_id
	WHERE	o.type = 'U' 
	AND		o.name = @TableName
	AND		s.name = @SchemaName
	AND		c.name = 'LastModifiedUserID'
	)
	SET @UpdateUserIDColumn = 'Inserted.LastModifiedUserID'
ELSE
	SET @UpdateUserIDColumn = 'NULL'
   
--------------------------------------------------------------------------------------------
IF (UPPER(@BuildOnly) = 'I' OR @BuildOnly = '')
BEGIN
	-- drop existing insert trigger
	SET @SQL = 'If EXISTS (Select * from sys.objects o join sys.schemas s on o.schema_id = s.schema_id  '
		   + ' where s.name = ''' + @SchemaName + ''''
		   + '   and o.name = ''' + @TableName + '_AuditBasic_Insert' + ''' )'
		   + ' DROP TRIGGER ' + @SchemaName + '.' + @TableName + '_AuditBasic_Insert'
	EXEC (@SQL)

	-- build insert trigger 
	SET @SQL = 'CREATE TRIGGER ' + @SchemaName + '.' + @TableName + '_AuditBasic_Insert' + ' ON ['+ @SchemaName + '].[' + @TableName + ']' + Char(13) + Char(10)
		   + ' AFTER Insert' + Char(13) + Char(10) + ' NOT FOR REPLICATION' + Char(13) + Char(10)
		   + ' AS ' + Char(13) + Char(10)
		   + ' BEGIN ' + Char(13) + Char(10)
		   + ' SET NOCOUNT ON ' + Char(13) + Char(10)
		   + ' SET ARITHABORT ON ' + Char(13) + Char(10)
	      
		   + ' -- patterned after AutoAudit created by Paul Nielsen ' + Char(13) + Char(10)
		   + ' -- www.SQLServerBible.com ' + Char(13) + Char(10) + Char(13) + Char(10)
		   + 'DECLARE @AuditTime DATETIME' + Char(13) + Char(10)
		   + 'SET @AuditTime = GetDate()' + Char(13) + Char(10) + Char(13) + Char(10)
	       
		   + ' BEGIN TRY ' + Char(13) + Char(10)
	       
	-- Only capture SQL Statement if submitted by a user other than BotanicusService and MOBOT\SQLSERVER
	SELECT @SQL = @SQL + ' DECLARE @UserSQL nvarchar(max)' + Char(13) + Char(10) +
			' SET @UserSQL = ''''' + Char(13) + Char(10)
			
	SELECT @SQL = @SQL + 
			 ' IF (SUSER_NAME() <> ''BotanicusService'' AND SUSER_NAME() <> ''BHLWebUser'' AND SUSER_NAME() <> ''MOBOT\SQLSERVER'')' + Char(13) + Char(10)
		   + ' BEGIN' + Char(13) + Char(10)
		   + '  -- capture SQL Statement' + Char(13) + Char(10)
		   + '  DECLARE @ExecStr varchar(50)' + Char(13) + Char(10)
		   + '  DECLARE  @inputbuffer TABLE (EventType nvarchar(30), Parameters int, EventInfo nvarchar(max))' + Char(13) + Char(10)
		   + '  SET @ExecStr = ''DBCC INPUTBUFFER(@@SPID) with no_infomsgs''' + Char(13) + Char(10)
		   + '  INSERT INTO @inputbuffer EXEC (@ExecStr)' + Char(13) + Char(10)
		   + '  SELECT @UserSQL = EventInfo FROM @inputbuffer' + Char(13) + Char(10)
		   + ' END' + Char(13) + Char(10)
		   + Char(13) + Char(10)      

	SELECT @SQL = @SQL + 
			' INSERT audit.AuditBasic (AuditDate, SystemUserID, EntityName, Operation, SQLStatement, EntityKey1'

	IF (@PKColumnName2 IS NOT NULL) SELECT @SQL = @SQL + ', EntityKey2'
	IF (@PKColumnName3 IS NOT NULL) SELECT @SQL = @SQL + ', EntityKey3'

	SELECT @SQL = @SQL + 
			', ApplicationUserID)' + Char(13) + Char(10)
			+ ' SELECT @AuditTime, SUSER_SNAME(), ''' + @SchemaName + '.' + @TableName + ''', ''I'','
			+ '@UserSQL, Inserted.' + @PKColumnName1

	IF (@PKColumnName2 IS NOT NULL) SELECT @SQL = @SQL + ', Inserted.' + @PKColumnName2
	IF (@PKColumnName3 IS NOT NULL) SELECT @SQL = @SQL + ', Inserted.' + @PKColumnName3

	SELECT @SQL = @SQL + 
		',' + @InsertUserIDColumn + Char(13) + Char(10)
			+ ' FROM Inserted' + Char(13) + Char(10)
		   + Char(13) + Char(10)
		
	SELECT @SQL = @SQL + 
	   + ' END TRY ' + Char(13) + Char(10)
	   + ' BEGIN CATCH ' + Char(13) + Char(10)
	   + '   DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT' + Char(13) + Char(10) 

	   + '   SET @ErrorMessage = ERROR_MESSAGE()  ' + Char(13) + Char(10)
	   + '   SET @ErrorSeverity = ERROR_SEVERITY() ' + Char(13) + Char(10) 
	   + '   SET @ErrorState = ERROR_STATE()  ' + Char(13) + Char(10)
	   + '   RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) WITH LOG' + Char(13) + Char(10) 
	   + ' END CATCH '       + Char(13) + Char(10)
	   + ' END '

	EXEC (@SQL)

	SET @SQL = '[' + @SchemaName + '].[' + @TableName + '_AuditBasic_Insert]'

	EXEC sp_settriggerorder 
	  @triggername= @SQL, 
	  @order='Last', 
	  @stmttype = 'INSERT'
END

--------------------------------------------------------------------------------------------
IF (UPPER(@BuildOnly) = 'U' OR @BuildOnly = '')
BEGIN
	-- drop existing update trigger
	SET @SQL = 'If EXISTS (Select * from sys.objects o join sys.schemas s on o.schema_id = s.schema_id  '
		   + ' where s.name = ''' + @SchemaName + ''''
		   + '   and o.name = ''' + @TableName + '_AuditBasic_Update' + ''' )'
		   + ' DROP TRIGGER ' + @SchemaName + '.' + @TableName + '_AuditBasic_Update'
	EXEC (@SQL)

	-- build update trigger 
	SET @SQL = 'CREATE TRIGGER ' + @SchemaName + '.' + @TableName + '_AuditBasic_Update' + ' ON ['+ @SchemaName + '].[' + @TableName + ']' + Char(13) + Char(10)
		   + ' AFTER Update' + Char(13) + Char(10) + ' NOT FOR REPLICATION' + Char(13) + Char(10)
		   + ' AS ' + Char(13) + Char(10)
		   + ' BEGIN ' + Char(13) + Char(10)
		   + ' SET NOCOUNT ON ' + Char(13) + Char(10)

		   + ' -- patterned after AutoAudit created by Paul Nielsen ' + Char(13) + Char(10)
		   + ' -- www.SQLServerBible.com ' + Char(13) + Char(10) + Char(13) + Char(10)
		   + 'DECLARE @AuditTime DATETIME, @IsDirty BIT' + Char(13) + Char(10)
		   + 'SET @AuditTime = GetDate()' + Char(13) + Char(10) + Char(13) + Char(10)
		   + 'SET @IsDirty = 0' + Char(13) + Char(10) + Char(13) + Char(10)
	       
		   + ' BEGIN TRY ' + Char(13) + Char(10)

	-- Only capture SQL Statement if submitted by a user other than BotanicusService and MOBOT\SQLSERVER
	SELECT @SQL = @SQL + ' DECLARE @UserSQL nvarchar(max)' + Char(13) + Char(10) +
			' SET @UserSQL = ''''' + Char(13) + Char(10)

	SELECT @SQL = @SQL + 
			 ' IF (SUSER_NAME() <> ''BotanicusService'' AND SUSER_NAME() <> ''BHLWebUser'' AND SUSER_NAME() <> ''MOBOT\SQLSERVER'')' + Char(13) + Char(10)
		   + ' BEGIN' + Char(13) + Char(10)
		   + '  -- capture SQL Statement' + Char(13) + Char(10)
		   + '  DECLARE @ExecStr varchar(50)' + Char(13) + Char(10)
		   + '  DECLARE  @inputbuffer TABLE (EventType nvarchar(30), Parameters int, EventInfo nvarchar(max))' + Char(13) + Char(10)
		   + '  SET @ExecStr = ''DBCC INPUTBUFFER(@@SPID) with no_infomsgs''' + Char(13) + Char(10)
		   + '  INSERT INTO @inputbuffer EXEC (@ExecStr)' + Char(13) + Char(10)
		   + '  SELECT @UserSQL = EventInfo FROM @inputbuffer' + Char(13) + Char(10)
		   + ' END' + Char(13) + Char(10)
		   + Char(13) + Char(10)      

	SELECT @SQL = @SQL + 
			' INSERT audit.AuditBasic (AuditDate, SystemUserID, EntityName, Operation, SQLStatement, EntityKey1'

	IF (@PKColumnName2 IS NOT NULL) SELECT @SQL = @SQL + ', EntityKey2'
	IF (@PKColumnName3 IS NOT NULL) SELECT @SQL = @SQL + ', EntityKey3'

	SELECT @SQL = @SQL + 
			', ApplicationUserID)' + Char(13) + Char(10)
			+ ' SELECT @AuditTime, SUSER_SNAME(), ''' + @SchemaName + '.' + @TableName + ''', ''U'','
			+ '@UserSQL, Inserted.' + @PKColumnName1

	IF (@PKColumnName2 IS NOT NULL) SELECT @SQL = @SQL + ', Inserted.' + @PKColumnName2
	IF (@PKColumnName3 IS NOT NULL) SELECT @SQL = @SQL + ', Inserted.' + @PKColumnName3

	SELECT @SQL = @SQL + 
			',' + @UpdateUserIDColumn + Char(13) + Char(10)
			+ ' FROM Inserted' + Char(13) + Char(10)
		   + Char(13) + Char(10)
		
	SELECT @SQL = @SQL + 
	   + ' END TRY ' + Char(13) + Char(10)
	   + ' BEGIN CATCH ' + Char(13) + Char(10)
	   + '   DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT' + Char(13) + Char(10) 

	   + '   SET @ErrorMessage = ERROR_MESSAGE()  ' + Char(13) + Char(10)
	   + '   SET @ErrorSeverity = ERROR_SEVERITY() ' + Char(13) + Char(10) 
	   + '   SET @ErrorState = ERROR_STATE()  ' + Char(13) + Char(10)
	   + '   RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) WITH LOG' + Char(13) + Char(10) 
	   + ' END CATCH '  + Char(13) + Char(10) 
	   + ' END '

	EXEC (@SQL)

	SET @SQL = '[' + @SchemaName + '].[' + @TableName + '_AuditBasic_Update]'

	EXEC sp_settriggerorder 
	  @triggername= @SQL, 
	  @order='Last', 
	  @stmttype = 'UPDATE'
END

--------------------------
IF (UPPER(@BuildOnly) = 'D' OR @BuildOnly = '')
BEGIN
	-- drop existing delete trigger
	SET @SQL = 'If EXISTS (Select * from sys.objects o join sys.schemas s on o.schema_id = s.schema_id  '
		   + ' where s.name = ''' + @SchemaName + ''''
		   + '   and o.name = ''' + @TableName + '_AuditBasic_Delete' + ''' )'
		   + ' DROP TRIGGER ' + @SchemaName + '.' + @TableName + '_AuditBasic_Delete'
	EXEC (@SQL)

	-- build delete trigger 
	SET @SQL = 'CREATE TRIGGER ' + @SchemaName + '.' + @TableName + '_AuditBasic_Delete' + ' ON ['+ @SchemaName + '].[' + @TableName + ']' + Char(13) + Char(10)
		   + ' AFTER Delete' + Char(13) + Char(10) + ' NOT FOR REPLICATION' + Char(13) + Char(10)
		   + ' AS ' + Char(13) + Char(10)
		   + ' BEGIN ' + Char(13) + Char(10)
		   + ' SET NOCOUNT ON ' + Char(13) + Char(10)

		   + ' -- patterned after AutoAudit created by Paul Nielsen ' + Char(13) + Char(10)
		   + ' -- www.SQLServerBible.com ' + Char(13) + Char(10) + Char(13) + Char(10)
		   + 'DECLARE @AuditTime DATETIME' + Char(13) + Char(10)
		   + 'SET @AuditTime = GetDate()' + Char(13) + Char(10) + Char(13) + Char(10)
	
	-- Only capture SQL Statement if submitted by a user other than BotanicusService and MOBOT\SQLSERVER
	SELECT @SQL = @SQL + ' DECLARE @UserSQL nvarchar(max)' + Char(13) + Char(10) +
			' SET @UserSQL = ''''' + Char(13) + Char(10)

	SELECT @SQL = @SQL + 
			 ' IF (SUSER_NAME() <> ''BotanicusService'' AND SUSER_NAME() <> ''BHLWebUser'' AND SUSER_NAME() <> ''MOBOT\SQLSERVER'')' + Char(13) + Char(10)
		   + ' BEGIN' + Char(13) + Char(10)
		   + '  -- capture SQL Statement' + Char(13) + Char(10)
		   + '  DECLARE @ExecStr varchar(50)' + Char(13) + Char(10)
		   + '  DECLARE  @inputbuffer TABLE (EventType nvarchar(30), Parameters int, EventInfo nvarchar(max))' + Char(13) + Char(10)
		   + '  SET @ExecStr = ''DBCC INPUTBUFFER(@@SPID) with no_infomsgs''' + Char(13) + Char(10)
		   + '  INSERT INTO @inputbuffer EXEC (@ExecStr)' + Char(13) + Char(10)
		   + '  SELECT @UserSQL = EventInfo FROM @inputbuffer' + Char(13) + Char(10)
		   + ' END' + Char(13) + Char(10)
		   + Char(13) + Char(10)      
	       
	SELECT @SQL = @SQL + 
		   	' BEGIN TRY ' + Char(13) + Char(10)

	SELECT @SQL = @SQL + 
			 ' INSERT audit.AuditBasic (AuditDate, SystemUserID, EntityName, Operation, SQLStatement, EntityKey1'
			 
	IF (@PKColumnName2 IS NOT NULL) SELECT @SQL = @SQL + ', EntityKey2'
	IF (@PKColumnName3 IS NOT NULL) SELECT @SQL = @SQL + ', EntityKey3'
	
	SELECT @SQL = @SQL + 
			', ApplicationUserID)' + Char(13) + Char(10)
			+ ' SELECT @AuditTime, SUSER_SNAME(), ''' + @SchemaName + '.' + @TableName + ''', ''D'','
			+ '@UserSQL, Deleted.' + @PKColumnName1
			
	IF (@PKColumnName2 IS NOT NULL) SELECT @SQL = @SQL + ', Deleted.' + @PKColumnName2
	IF (@PKColumnName3 IS NOT NULL) SELECT @SQL = @SQL + ', Deleted.' + @PKColumnName3
	
	SELECT @SQL = @SQL + ',NULL ' + Char(13) + Char(10)
			+ ' FROM Deleted ' + Char(13) + Char(10)
		   + Char(13) + Char(10)

	SELECT @SQL = @SQL + 
	   + ' END TRY ' + Char(13) + Char(10)
	   + ' BEGIN CATCH ' + Char(13) + Char(10)
	   + '   DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT' + Char(13) + Char(10) 

	   + '   SET @ErrorMessage = ERROR_MESSAGE()  ' + Char(13) + Char(10)
	   + '   SET @ErrorSeverity = ERROR_SEVERITY() ' + Char(13) + Char(10) 
	   + '   SET @ErrorState = ERROR_STATE()  ' + Char(13) + Char(10)
	   + '   RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) WITH LOG' + Char(13) + Char(10) 
	   + ' END CATCH '  + Char(13) + Char(10) 
	   + ' END '

	EXEC (@SQL)

	SET @SQL = '[' + @SchemaName + '].[' + @TableName + '_AuditBasic_Delete]'

	EXEC sp_settriggerorder 
	  @triggername= @SQL, 
	  @order='Last', 
	  @stmttype = 'DELETE'
END
  
END


