CREATE PROC [audit].[AuditBasicDropTrigger] (
   @SchemaName SYSNAME  = 'dbo',
   @TableName SYSNAME
) 
AS 
BEGIN

SET NOCOUNT ON

DECLARE @SQL NVARCHAR(max)

-- drop existing insert trigger
SET @SQL = 'If EXISTS (Select * from sys.objects o join sys.schemas s on o.schema_id = s.schema_id  '
       + ' where s.name = ''' + @SchemaName + ''''
       + '   and o.name = ''' + @TableName + '_AuditBasic_Insert' + ''' )'
       + ' DROP TRIGGER ' + @SchemaName + '.' + @TableName + '_AuditBasic_Insert'
EXEC (@SQL)

-- drop existing update trigger
SET @SQL = 'If EXISTS (Select * from sys.objects o join sys.schemas s on o.schema_id = s.schema_id  '
       + ' where s.name = ''' + @SchemaName + ''''
       + '   and o.name = ''' + @TableName + '_AuditBasic_Update' + ''' )'
       + ' DROP TRIGGER ' + @SchemaName + '.' + @TableName + '_AuditBasic_Update'
EXEC (@SQL)

-- drop existing delete trigger
SET @SQL = 'If EXISTS (Select * from sys.objects o join sys.schemas s on o.schema_id = s.schema_id  '
       + ' where s.name = ''' + @SchemaName + ''''
       + '   and o.name = ''' + @TableName + '_AuditBasic_Delete' + ''' )'
       + ' DROP TRIGGER ' + @SchemaName + '.' + @TableName + '_AuditBasic_Delete'
EXEC (@SQL)

END


