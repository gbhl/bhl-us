
CREATE PROCEDURE [audit].[AuditBasicArchiveAndSummarize]

AS

BEGIN

SET NOCOUNT ON

------------------------------------------------------------------
-- 1) Move new rows over to the archive table
-- 2) Remove rows older than 15 days from the "live" audit table
-- 3) Remove summarized data for rows that have been deleted
-- 4) Summarize older audit data
------------------------------------------------------------------

DECLARE @MaxAge int
SET @MaxAge = 15			-- Max age in days of rows in the audit table
DECLARE @MaxSummaryAge int
SET @MaxSummaryAge = 180	-- Max age in days of rows in the audit summary table

-- Get today's date (omit time)
DECLARE @Today datetime
SET @Today = CONVERT(datetime, CONVERT(nvarchar(20), GETDATE(), 101))

-- STEP 1

-- Move all unmoved rows prior to today's date over to the audit archive database
INSERT	audit.AuditBasicArchive (AuditBasicID, AuditDate, EntityName, 
		Operation, EntityKey1, EntityKey2, EntityKey3, ApplicationUserID, 
		SystemUserID, SQLStatement)
SELECT	a.AuditBasicID,
		a.AuditDate,
		a.EntityName,
		a.Operation,
		a.EntityKey1,
		a.EntityKey2,
		a.EntityKey3,
		a.ApplicationUserID,
		a.SystemUserID,
		a.SQLStatement
FROM	audit.AuditBasic a LEFT JOIN audit.AuditBasicArchive arch
			ON a.AuditBasicID = arch.AuditBasicID
WHERE	arch.AuditBasicID IS NULL
AND		a.AuditDate < @Today

-- STEP 2

-- Delete moved rows older than 15 days from the "live" audit table
DELETE	audit.AuditBasic
FROM	audit.AuditBasic a INNER JOIN audit.AuditBasicArchive arch
			ON a.AuditBasicID = arch.AuditBasicID
WHERE	a.AuditDate < @Today - @MaxAge

DECLARE @MaxSummaryDate datetime
SELECT @MaxSummaryDate = ISNULL(MAX(AuditDate), '1/1/2011') FROM audit.AuditBasicSummary

-- STEP 3

-- Get the entity names and key values for newly deleted rows
SELECT	EntityName, EntityKey1, EntityKey2, EntityKey3 
INTO	#tmpDeleted
FROM	audit.AuditBasicArchive
WHERE	Operation = 'D' 
AND		AuditDate >= (@MaxSummaryDate + 1) AND AuditDate < (@Today - @MaxAge)

-- Delete any summary rows for records that have been deleted
DELETE	audit.AuditBasicSummary
FROM	audit.AuditBasicSummary s INNER JOIN #tmpDeleted d
			ON s.EntityName = d.EntityName
			AND s.EntityKey1 = d.EntityKey1
			AND ISNULL(s.EntityKey2, 'NULL') = ISNULL(d.EntityKey2, 'NULL')
			AND	ISNULL(s.EntityKey3, 'NULL') = ISNULL(d.EntityKey3, 'NULL')

-- STEP 4

-- Summarize any new data
INSERT	audit.AuditBasicSummary (AuditDate, EntityName, EntityKey1, EntityKey2, EntityKey3, 
								Operation, ApplicationUserID)
SELECT	CONVERT(DATETIME, CONVERT(NVARCHAR(20), a.auditdate, 101)) AS AuditDate, 
		a.EntityName, 
		a.EntityKey1, 
		a.EntityKey2,
		a.EntityKey3,
		a.Operation, 
		a.ApplicationUserID
FROM	audit.AuditBasicArchive a
		-- Exclude all auditing entries related to deleted rows.  Since individual column details are not
		-- tracked, there's no way to view details of deleted rows.  Therefore, there's no point to keeping
		-- that data around in the summary table.  Full details remain available in the BHLAuditArchive
		-- database.
		LEFT JOIN #tmpDeleted t 
			ON a.EntityName = t.EntityName 
			AND a.EntityKey1 = t.EntityKey1
			AND	ISNULL(a.EntityKey2, 'NULL') = ISNULL(t.EntityKey2, 'NULL')
			AND	ISNULL(a.EntityKey3, 'NULL') = ISNULL(t.EntityKey3, 'NULL')
WHERE	t.EntityName IS NULL
AND		a.AuditDate >= (@MaxSummaryDate + 1) AND a.AuditDate < (@Today - @MaxAge)
-- Ignore name updates
AND		a.SqlStatement NOT LIKE '%LastPageNameLookupDate%'	
GROUP BY
		CONVERT(DATETIME, CONVERT(NVARCHAR(20), a.AuditDate, 101)),
		a.EntityName, 
		a.EntityKey1, 
		a.EntityKey2,
		a.EntityKey3,
		a.Operation, 
		a.ApplicationUserID 

DROP TABLE #tmpDeleted

-- STEP 5

-- Delete summary rows older than the maximum summary age from the summary table.
-- If necessary, these rows can be recreated later from the archive table.
DELETE	
FROM	audit.AuditBasicSummary 
WHERE	AuditDate < @Today - @MaxSummaryAge

END

