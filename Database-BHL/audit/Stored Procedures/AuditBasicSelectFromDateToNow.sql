
CREATE PROCEDURE [audit].[AuditBasicSelectFromDateToNow]
@AuditDate DateTime 
AS 
BEGIN

-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

SELECT	AuditDate, EntityName, Operation, EntityKey1, EntityKey2, EntityKey3 
FROM	audit.AuditBasic 
WHERE	AuditDate > @AuditDate 
ORDER BY AuditDate

END


