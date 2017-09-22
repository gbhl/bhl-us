CREATE PROCEDURE [import].[ImportFileSelectDetails]

@ContributorCode nvarchar(10) = '',
@FileStatusID int = 0,
@NumDays int = 30

AS

BEGIN

SET NOCOUNT ON

SELECT	f.ImportFileID,
		f.ImportFileName,
		i.InstitutionName,
		ISNULL(u1.FirstName + ' ', '') + ISNULL(u1.LastName, '') AS CreationUser,
		ISNULL(u2.FirstName + ' ', '') + ISNULL(u2.LastName, '') AS LastModifiedUser,
		s.StatusName,
		f.LastModifiedDate,
		COUNT(*) TotalRecords,
		SUM(CASE WHEN r.ImportRecordStatusID = 10 THEN 1 ELSE 0 END) AS NewRecords,
		SUM(CASE WHEN r.ImportRecordStatusID = 20 THEN 1 ELSE 0 END) AS ImportedRecords,
		SUM(CASE WHEN r.ImportRecordStatusID = 30 THEN 1 ELSE 0 END) AS RejectedRecords,
		SUM(CASE WHEN r.ImportRecordStatusID = 40 THEN 1 ELSE 0 END) AS ErrorRecords,
		SUM(CASE WHEN r.ImportRecordStatusID = 50 THEN 1 ELSE 0 END) AS IncompleteRecords,
		SUM(CASE WHEN r.ImportRecordStatusID = 60 THEN 1 ELSE 0 END) AS DuplicateRecords,
		SUM(CASE WHEN r.ImportRecordStatusID = 70 THEN 1 ELSE 0 END) AS InvalidRecords
FROM	import.importFile f 
		INNER JOIN dbo.Institution i ON f.ContributorCode = i.InstitutionCode
		INNER JOIN import.ImportRecord r ON f.ImportFileID = r.ImportFileID
		INNER JOIN import.ImportFileStatus s ON f.ImportFileStatusID = s.ImportFileStatusID
		LEFT JOIN dbo.AspNetUsers u1 ON f.CreationUserID = u1.Id
		LEFT JOIN dbo.AspNetUsers u2 ON f.LastModifiedUserID = u2.Id
WHERE	f.ContributorCode = @ContributorCode OR @ContributorCode = ''
AND		f.ImportFileStatusID = @FileStatusID OR @FileStatusID = 0
AND		DATEDIFF(DAY, f.LastModifiedDate, GETDATE()) <= @NumDays
GROUP BY
		f.ImportFileID,
		f.ImportFileName,
		i.InstitutionName,
		u1.FirstName,
		u1.LastName,
		u2.FirstName,
		u2.LastName,
		s.StatusName,
		f.LastModifiedDate
ORDER BY
		f.LastModifiedDate DESC,
		i.InstitutionName

END
