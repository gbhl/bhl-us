CREATE PROCEDURE import.ImportFileSelectDetails

@ContributorCode nvarchar(10) = '',
@FileStatusID int = 0,
@NumDays int = 30

AS

BEGIN

SET NOCOUNT ON

SELECT	f.ImportFileID,
		f.ImportFileName,
		i.InstitutionName,
		s.StatusName,
		f.LastModifiedDate,
		COUNT(*) TotalRecords,
		SUM(CASE WHEN r.ImportRecordStatusID = 10 THEN 1 ELSE 0 END) AS NewRecords,
		SUM(CASE WHEN r.ImportRecordStatusID = 20 THEN 1 ELSE 0 END) AS ImportedRecords,
		SUM(CASE WHEN r.ImportRecordStatusID = 30 THEN 1 ELSE 0 END) AS RejectedRecords,
		SUM(CASE WHEN r.ImportRecordStatusID = 40 THEN 1 ELSE 0 END) AS ErrorRecords
FROM	import.importFile f 
		INNER JOIN dbo.Institution i ON f.ContributorCode = i.InstitutionCode
		INNER JOIN import.ImportRecord r ON f.ImportFileID = r.ImportFileID
		INNER JOIN import.ImportFileStatus s ON f.ImportFileStatusID = s.ImportFileStatusID
WHERE	f.ContributorCode = @ContributorCode OR @ContributorCode = ''
AND		f.ImportFileStatusID = @FileStatusID OR @FileStatusID = 0
AND		DATEDIFF(DAY, f.LastModifiedDate, GETDATE()) <= @NumDays
GROUP BY
		f.ImportFileID,
		f.ImportFileName,
		i.InstitutionName,
		s.StatusName,
		f.LastModifiedDate
ORDER BY
		f.LastModifiedDate DESC,
		i.InstitutionName

END
