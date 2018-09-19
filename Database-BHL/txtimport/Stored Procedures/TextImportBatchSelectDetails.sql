CREATE PROCEDURE [txtimport].[TextImportBatchSelectDetails]

@FileStatusID int = 0,
@NumDays int = 30

AS

BEGIN

SET NOCOUNT ON

SELECT	f.TextImportBatchID,
		ISNULL(u1.FirstName + ' ', '') + ISNULL(u1.LastName, '') AS CreationUser,
		ISNULL(u2.FirstName + ' ', '') + ISNULL(u2.LastName, '') AS LastModifiedUser,
		s.StatusName,
		f.LastModifiedDate,
		COUNT(*) TotalRecords,
		SUM(CASE WHEN r.TextImportBatchFileStatusID = 10 THEN 1 ELSE 0 END) AS ReadyRecords,
		SUM(CASE WHEN r.TextImportBatchFileStatusID = 20 THEN 1 ELSE 0 END) AS ReviewRecords,
		SUM(CASE WHEN r.TextImportBatchFileStatusID = 30 THEN 1 ELSE 0 END) AS ImportedRecords,
		SUM(CASE WHEN r.TextImportBatchFileStatusID = 40 THEN 1 ELSE 0 END) AS RejectedRecords,
		SUM(CASE WHEN r.TextImportBatchFileStatusID = 50 THEN 1 ELSE 0 END) AS ErrorRecords
FROM	txtimport.TextImportBatch f 
		INNER JOIN txtimport.TextImportBatchFile r ON f.TextImportBatchID = r.TextImportBatchID
		INNER JOIN txtimport.TextImportBatchStatus s ON f.TextImportBatchStatusID = s.TextImportBatchStatusID
		LEFT JOIN dbo.AspNetUsers u1 ON f.CreationUserID = u1.Id
		LEFT JOIN dbo.AspNetUsers u2 ON f.LastModifiedUserID = u2.Id
WHERE	(f.TextImportBatchStatusID = @FileStatusID OR @FileStatusID = 0)
AND		DATEDIFF(DAY, f.LastModifiedDate, GETDATE()) <= @NumDays
GROUP BY
		f.TextImportBatchID,
		u1.FirstName,
		u1.LastName,
		u2.FirstName,
		u2.LastName,
		s.StatusName,
		f.LastModifiedDate
ORDER BY
		f.LastModifiedDate DESC

END
