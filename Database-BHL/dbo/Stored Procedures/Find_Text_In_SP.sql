
CREATE PROCEDURE [dbo].[Find_Text_In_SP]
@StringToSearch varchar(100)
AS
   SET @StringToSearch = '%' +@StringToSearch + '%'
   SET @StringToSearch = REPLACE(REPLACE(@StringToSearch, '[', ''), ']', '')
   SELECT Distinct SO.Name
   FROM sysobjects SO (NOLOCK)
   INNER JOIN syscomments SC (NOLOCK) on SO.Id = SC.ID
   --AND SO.Type = 'P'
   AND REPLACE(REPLACE(SC.Text, '[', ''), ']', '') LIKE @stringtosearch 
   ORDER BY SO.Name

