CREATE PROCEDURE [txtimport].[TextImportBatchSelectDetails]

@FileStatusID int = 0,
@NumDays int = 30

AS

BEGIN

SET NOCOUNT ON

SELECT	b.TextImportBatchID,
		ISNULL(u1.FirstName + ' ', '') + ISNULL(u1.LastName, '') AS CreationUser,
		ISNULL(u2.FirstName + ' ', '') + ISNULL(u2.LastName, '') AS LastModifiedUser,
		s.StatusName,
		b.CreationDate,
		b.LastModifiedDate,
		COUNT(*) TotalRecords,
		SUM(CASE WHEN r.TextImportBatchFileStatusID = 10 THEN 1 ELSE 0 END) AS ReadyRecords,
		SUM(CASE WHEN r.TextImportBatchFileStatusID = 20 THEN 1 ELSE 0 END) AS ReviewRecords,
		SUM(CASE WHEN r.TextImportBatchFileStatusID = 30 THEN 1 ELSE 0 END) AS ImportedRecords,
		SUM(CASE WHEN r.TextImportBatchFileStatusID = 40 THEN 1 ELSE 0 END) AS RejectedRecords,
		SUM(CASE WHEN r.TextImportBatchFileStatusID = 50 THEN 1 ELSE 0 END) AS ErrorRecords
FROM	txtimport.TextImportBatch b 
		INNER JOIN txtimport.TextImportBatchFile r ON b.TextImportBatchID = r.TextImportBatchID
		INNER JOIN txtimport.TextImportBatchStatus s ON b.TextImportBatchStatusID = s.TextImportBatchStatusID
		LEFT JOIN dbo.AspNetUsers u1 ON b.CreationUserID = u1.Id
		LEFT JOIN dbo.AspNetUsers u2 ON b.LastModifiedUserID = u2.Id
WHERE	(b.TextImportBatchStatusID = @FileStatusID OR @FileStatusID = 0)
AND		DATEDIFF(DAY, b.LastModifiedDate, GETDATE()) <= @NumDays
GROUP BY
		b.TextImportBatchID,
		u1.FirstName,
		u1.LastName,
		u2.FirstName,
		u2.LastName,
		s.StatusName,
		b.CreationDate,
		b.LastModifiedDate
ORDER BY
		b.CreationDate DESC

END
